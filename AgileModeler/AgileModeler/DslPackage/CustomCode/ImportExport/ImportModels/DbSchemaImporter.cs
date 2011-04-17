using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Practices.RepositoryFactory.SchemaDiscovery;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base;
using Microsoft.VisualStudio.Modeling.Diagrams;

using AgileFx.AgileModeler;
using AgileFx.AgileModeler.DslPackage.CustomCode.Forms;
using System.Text.RegularExpressions;
using AgileFx.AgileModeler.DslPackage.Utils;
using AgileFx.AgileModeler.CustomCode;

namespace Utilities
{
    public class DbSchemaImporter
    {
        private ConnectionStringSettings connSettings = null;
        private Diagram diagram = null;
        Func<string, string> capitalize = word => string.IsNullOrEmpty(word) ? string.Empty : word.Substring(0, 1).ToUpper() + word.Substring(1);

        public DbSchemaImporter(Diagram diagram)
        {
            this.diagram = diagram;
            string connectionString = ((ModelRoot)diagram.ModelElement).ConnectionString;
            if (!string.IsNullOrEmpty(connectionString))
            {
                this.connSettings = new ConnectionStringSettings("default", connectionString, "System.Data.SqlClient");
            }
        }

        public bool FullDatabaseReload { get; set; }

        public bool ImportModels()
        {
            var existingShapes = diagram.NestedChildShapes.ToArray();
            if (LoadModelsFromDb())
            {
                var newShapes = diagram.NestedChildShapes.Where(s => !existingShapes.Contains(s)).ToList();
                DiagramUtil.AutoLayout(newShapes, diagram);
                return true;
            }
            return false;
        }

        bool LoadModelsFromDb()
        {
            //Supports only MSSQL for now..
            var schemaDiscoverer = new MSSQLSchemaDiscover(connSettings);
            var allTables = schemaDiscoverer.DiscoverTables();
            List<Table> modelTables = new List<Table>();
            List<Table> mappingTables = new List<Table>();
            foreach (Table t in allTables)
            {
                if (t.Columns.Count == 2 && t.Columns.All(c => c.IsPrimaryKey))
                    mappingTables.Add(t);
                else
                    modelTables.Add(t);
            }

            var classesInStore = diagram.Store.ElementDirectory.FindElements<ModelClass>().ToList();
            List<Table> tablesSelected = null;
            bool autoDetectInheritance = true;
            string tablePrefix = "";

            var customInheritances = new Dictionary<string, string>();
            if (this.FullDatabaseReload)
            {
                tablesSelected = new List<Table>(modelTables);

                var origCursor2 = Cursor.Current;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    using (var tx = diagram.Store.TransactionManager.BeginTransaction("FullScope", false))
                    {
                        classesInStore.ForEach(c =>
                        {
                            if (tablesSelected.Find(t => t.Name == c.TableName) != null)
                            {
                                customInheritances.Add(c.TableName, c.DerivesOrImplements);
                                c.Delete();
                            }
                        });
                        if (tx.HasPendingChanges) tx.Commit();
                    }
                }
                finally
                {
                    Cursor.Current = origCursor2;
                }
                classesInStore = diagram.Store.ElementDirectory.FindElements<ModelClass>().ToList();
            }
            else
            {
                var tableSelectionDlg = new TableSelectionForm();
                tableSelectionDlg.Initialize(modelTables, classesInStore.Select(c => c.TableName).ToList());
                if (tableSelectionDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tablesSelected = tableSelectionDlg.SelectedTables;
                    autoDetectInheritance = tableSelectionDlg.AutoDetectInheritance;
                    tablePrefix = tableSelectionDlg.TablePrefix;
                }
                else
                    return false;
            }
            var associations = schemaDiscoverer.DiscoverAssociations();

            var associationsInStore = diagram.Store.ElementDirectory.FindElements<AgileFx.AgileModeler.Association>().ToList();
            var inheritancesInStore = diagram.Store.ElementDirectory.FindElements<Inheritance>().ToList();

            Func<string, ModelClass> findClass = tableName => classesInStore.Find(c => c.TableName == tableName);
            Func<string, AgileFx.AgileModeler.Association> findAssociation = assocName => associationsInStore.Find(a => a.Name == assocName);

            Func<string, Table> findTable = tableName => tablesSelected.Find(t => t.Name == tableName);
            Func<Table, string, Column> getColumnFromTable = (table, columnName) => table.Columns.Find(c => c.Name == columnName);

            Func<string, string, bool> isColumnPrimary = (tableName, columnName) =>
            {
                var t = findTable(tableName);
                if (t != null)
                {
                    return getColumnFromTable(t, columnName).IsPrimaryKey;
                }
                else
                {
                    var c = findClass(tableName);
                    return (c.Baseclass != null)
                        ? inheritancesInStore.Find(inh => inh.Subclass == c).DerivedClassPrimaryKeyColumn == columnName
                        : c.Fields.Find(f => f.ColumnName == columnName).IsPrimaryKey;
                }
            };

            var unicodeDbTypes = new[] { DatabaseType.NChar, DatabaseType.NText, DatabaseType.NVarChar };
            var fixedLengthDbTypes = new[] { DatabaseType.Char, DatabaseType.NChar };
            var nonComparableTypes = new[] { BuiltInTypes.Binary, BuiltInTypes.Timestamp };
            var pagedStringTypes = new[] { DatabaseType.NText, DatabaseType.Text };

            Action<ModelField, Column> loadFieldFromColumn = (field, col) =>
            {
                field.IsPrimaryKey = col.IsPrimaryKey;
                field.Nullable = col.IsNullable;
                field.Type = col.DbDataType.Type == typeof(byte[]) ? BuiltInTypes.Binary : (BuiltInTypes)Enum.Parse(typeof(BuiltInTypes), col.DbDataType.Type.Name);
                field.IsDbGenerated = col.IsPrimaryKey;
                field.IsFixedLength = fixedLengthDbTypes.Contains(col.DbDataType.ProviderType);
                field.IsUnicode = unicodeDbTypes.Contains(col.DbDataType.ProviderType);
                field.IsUnique = col.IsUnique;
                field.MaxLength = pagedStringTypes.Contains(col.DbDataType.ProviderType) ? 0 : col.DbDataType.Size;
                field.IsIdentity = col.IsIdentity;
                if (nonComparableTypes.Any(t => t == field.Type)
                    || (field.Type == BuiltInTypes.String && field.MaxLength == 0))
                {
                    field.UpdateCheck = ConcurrencyCheckFrequency.Never;
                }
            };

            Action<Column, ModelClass> addFieldFromColumn = (col, modelClass) =>
            {
                var field = new ModelField(diagram.Store)
                {
                    ColumnName = col.Name,
                    Name = ModelUtil.GetMemberName(GetMemberName(col.Name, false), modelClass, false)
                };
                modelClass.Fields.Add(field);
                loadFieldFromColumn(field, col);
            };

            Action<ModelClass, string> clearTempProp = (modelClass, association) => modelClass.NavigationProperties.ToList().ForEach(np =>
            {
                if (np.Association == association && np.Type == "Temporary") np.Delete();
            });

            var origCursor = Cursor.Current;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                var selectedTableNames = tablesSelected.Select(t => t.Name).ToList();
                var tablesInScope = classesInStore.Select(c => c.TableName).Union(selectedTableNames).ToList();

                //At least one end of the mapping table should be a table in scope
                var mapTableAssociations = associations.Where(a => mappingTables.Any(t => t.Name == a.ChildTable)
                        && tablesInScope.Contains(a.ParentTable)).ToList();

                //Filter associations involving only tables newly added or already in store
                associations.ToList().ForEach(a =>
                {
                    //At least one end of the association should be a selected table 
                    //while the other end could be an existing table in store.
                    if (!((selectedTableNames.Contains(a.ParentTable) && tablesInScope.Contains(a.ChildTable))
                        || (tablesInScope.Contains(a.ParentTable) && selectedTableNames.Contains(a.ChildTable))))
                        associations.Remove(a);
                });


                var orderedTables = new List<Table>(tablesSelected);
                var inheritanceTable = new Dictionary<string, Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base.Association>();
                if (autoDetectInheritance)
                {
                    associations.ToList().ForEach(assoc =>
                    {
                        if (isColumnPrimary(assoc.ChildTable, assoc.ChildMember) && assoc.ChildTable != assoc.ParentTable)
                        {
                            inheritanceTable.Add(assoc.ChildTable, assoc);
                            associations.Remove(assoc);
                        }
                    });
                    var newList = new List<Table>();
                    while (orderedTables.Count > 0)
                    {
                        orderedTables.ToList().ForEach(t =>
                        {
                            if (!inheritanceTable.ContainsKey(t.Name)
                                || newList.Any(nt => nt.Name == inheritanceTable[t.Name].ParentTable)
                                || !orderedTables.Any(ot => ot.Name == inheritanceTable[t.Name].ParentTable))
                            {
                                newList.Add(t);
                                orderedTables.Remove(t);
                            }
                        });
                    }
                    orderedTables = newList;
                }

                orderedTables.ForEach(t =>
                {
                    using (var tx = diagram.Store.TransactionManager.BeginTransaction("AddModel", false))
                    {
                        var cls = findClass(t.Name);
                        if (cls == null)
                        {
                            cls = new ModelClass(diagram.Store) { Name = GetClassName(t.Name, tablePrefix), TableName = t.Name };
                            ((ModelRoot)diagram.ModelElement).Types.Add(cls);
                            classesInStore.Add(cls);
                            if (this.FullDatabaseReload && customInheritances.ContainsKey(cls.TableName))
                                cls.DerivesOrImplements = customInheritances[cls.TableName];
                        }
                        if (inheritanceTable.ContainsKey(t.Name))
                        {
                            var assoc = inheritanceTable[t.Name];
                            if (cls.Baseclass == null)
                            {
                                var existingAssociation = findAssociation(assoc.Name);
                                if (existingAssociation != null) DeleteAssociation(existingAssociation);

                                new Inheritance(findClass(assoc.ParentTable), cls)
                                {
                                    DerivedClassPrimaryKeyColumn = assoc.ChildMember,
                                    BaseClassPrimaryKeyColumn = assoc.ParentMember
                                };
                            }
                            else associations.Add(assoc);
                        }

                        foreach (Column col in GetOrderedColumns(t))
                        {
                            var field = cls.Fields.Find(f => f.ColumnName == col.Name);
                            if (field != null && !field.IsEdited)
                                loadFieldFromColumn(field, col);
                            else
                                addFieldFromColumn(col, cls);
                        }
                        if (cls.Baseclass != null)
                            cls.Fields.Where(f => f.IsPrimaryKey).ToList().ForEach(f => f.Delete());

                        Func<string, string> findClassOrTableName = tableName =>
                        {
                            var c = findClass(tableName);
                            return (c != null) ? c.Name : GetClassName(tableName, tablePrefix);
                        };
                        associations.Where(a => findAssociation(a.Name) == null).ToList()
                        .ForEach(a =>
                        {
                            if (a.ParentTable == cls.TableName)
                                cls.NavigationProperties.Add(new NavigationProperty(diagram.Store) { Name = ModelUtil.GetMemberName(findClassOrTableName(a.ChildTable), cls, true), Association = a.Name, Type = "Temporary" });
                            else if (a.ChildTable == cls.TableName)
                                cls.NavigationProperties.Add(new NavigationProperty(diagram.Store) { Name = ModelUtil.GetMemberName(findClassOrTableName(a.ParentTable), cls, false), Association = a.Name, Type = "Temporary" });
                        });
                        mapTableAssociations.Where(a => findAssociation(a.ChildTable) == null).ToList()
                        .ForEach(a =>
                        {
                            if (a.ParentTable == cls.TableName)
                            {
                                var end2Assoc = mapTableAssociations.Find(x => x.Name != a.Name && x.ChildTable == a.ChildTable);
                                if (end2Assoc != null) cls.NavigationProperties.Add(new NavigationProperty(diagram.Store) { Name = ModelUtil.GetMemberName(findClassOrTableName(end2Assoc.ParentTable), cls, true), Association = a.ChildTable, Type = "Temporary" });
                            }
                        });

                        tx.Commit();
                    }
                });

                using (var tx = diagram.Store.TransactionManager.BeginTransaction("AddAssociations", false))
                {
                    mappingTables.ForEach(t =>
                    {
                        if (findAssociation(t.Name) != null) return;
                        var field1Association = mapTableAssociations.Find(a => a.ChildTable == t.Name && a.ChildMember == t.Columns[0].Name);
                        var field2Association = mapTableAssociations.Find(a => a.ChildTable == t.Name && a.ChildMember == t.Columns[1].Name);
                        if (field1Association != null && field2Association != null)
                        {
                            var class1 = findClass(field1Association.ParentTable);
                            var class2 = findClass(field2Association.ParentTable);

                            if (class1 == null || class2 == null) return;

                            clearTempProp(class1, t.Name);
                            var field1AssocNameParts = field1Association.Name.Split('_');
                            var class1NavProp = new NavigationProperty(diagram.Store)
                            {
                                ModelClass = class1,
                                Name = (field1AssocNameParts.Length >= 5) ? field1AssocNameParts[4] : ModelUtil.GetMemberName(class2.Name, class1, true),
                                Type = "ICollection<" + class2.Name + ">",
                                Multiplicity = AgileFx.AgileModeler.Multiplicity.ZeroMany,
                                Association = t.Name,
                            };

                            clearTempProp(class2, t.Name);
                            var field2AssocNameParts = field2Association.Name.Split('_');
                            var class2NavProp = new NavigationProperty(diagram.Store)
                            {
                                ModelClass = class2,
                                Name = (field2AssocNameParts.Length >= 5) ? field2AssocNameParts[4] : ModelUtil.GetMemberName(class1.Name, class2, true),
                                Type = "ICollection<" + class1.Name + ">",
                                Multiplicity = AgileFx.AgileModeler.Multiplicity.ZeroMany,
                                Association = t.Name
                            };

                            associations.Remove(field1Association);
                            associations.Remove(field2Association);
                            Func<string, string> getM2MFieldName = colName => Regex.Match(colName, ".+_?id$", RegexOptions.IgnoreCase).Success ? colName : colName + "Id";
                            new AgileFx.AgileModeler.Association(class1, class2)
                            {
                                Name = t.Name,

                                End1Multiplicity = AgileFx.AgileModeler.Multiplicity.ZeroMany,
                                End1RoleName = class1NavProp.ModelClass.Name,
                                End1NavigationProperty = class1NavProp.Name,

                                End2Multiplicity = AgileFx.AgileModeler.Multiplicity.ZeroMany,
                                End2RoleName = class2NavProp.ModelClass.Name,
                                End2NavigationProperty = class2NavProp.Name,

                                ManyToManyMappingTable = t.Name,
                                End1ManyToManyMappingColumn = t.Columns[0].Name,
                                End1ManyToManyFieldName = getM2MFieldName(t.Columns[0].Name),
                                End1ManyToManyNavigationProperty = GetMemberName(t.Columns[0].Name, true),
                                End2ManyToManyMappingColumn = t.Columns[1].Name,
                                End2ManyToManyFieldName = getM2MFieldName(t.Columns[1].Name),
                                End2ManyToManyNavigationProperty = GetMemberName(t.Columns[1].Name, true),
                            };
                        }
                    });

                    associations.ForEach(assoc =>
                    {
                        var parentClass = findClass(assoc.ParentTable);
                        var childClass = findClass(assoc.ChildTable);

                        var foreignkeyField = childClass.Fields.Find(f => f.ColumnName == assoc.ChildMember);
                        if (foreignkeyField != null && !foreignkeyField.IsEdited && !foreignkeyField.Name.ToLower().EndsWith("id")) foreignkeyField.Name += "Id";
                        var isOneToOneAssociation = (foreignkeyField != null && foreignkeyField.IsPrimaryKey);
                        var primaryEndMultiplicity = isOneToOneAssociation ? Multiplicity.ZeroOne : Multiplicity.ZeroMany;

                        if (findAssociation(assoc.Name) != null) return;

                        var assocNameParts = assoc.Name.Split('_');
                        if (assocNameParts.Length > 5)
                        {
                            isOneToOneAssociation = true;
                            primaryEndMultiplicity = assocNameParts[5].StartsWith("ZeroOne") ? Multiplicity.ZeroOne : Multiplicity.One;
                        }
                        clearTempProp(parentClass, assoc.Name);
                        var parentNavProp = new NavigationProperty(diagram.Store)
                        {
                            ModelClass = parentClass,
                            Name = (assocNameParts.Length >= 5) ? assocNameParts[4] : ModelUtil.GetMemberName(assoc.ChildTable, parentClass, true),
                            Type = isOneToOneAssociation ? childClass.Name : "ICollection<" + childClass.Name + ">",
                            Multiplicity = primaryEndMultiplicity,
                            Association = assoc.Name,
                        };

                        clearTempProp(childClass, assoc.Name);
                        var childNavProp = new NavigationProperty(diagram.Store)
                        {
                            ModelClass = childClass,
                            Name = (assocNameParts.Length >= 5) ? assocNameParts[2] : ModelUtil.GetMemberName(assoc.ParentTable, childClass, false),
                            Type = parentClass.Name,
                            Multiplicity = findTable(assoc.ChildTable).Columns.Find(c => c.Name == assoc.ChildMember).IsNullable ? Multiplicity.ZeroOne : Multiplicity.One,
                            Association = assoc.Name,
                            IsForeignkey = true,
                            ForeignkeyColumn = assoc.ChildMember,
                        };

                        new AgileFx.AgileModeler.Association(parentClass, childClass)
                        {
                            Name = assoc.Name,

                            End1Multiplicity = childNavProp.Multiplicity,
                            End1RoleName = parentNavProp.ModelClass.Name,
                            End1NavigationProperty = parentNavProp.Name,

                            End2Multiplicity = parentNavProp.Multiplicity,
                            End2RoleName = childNavProp.ModelClass.Name,
                            End2NavigationProperty = childNavProp.Name
                        };
                    });
                    if (tx.HasPendingChanges) tx.Commit();
                }

                return true;
            }
            finally
            {
                Cursor.Current = origCursor;
            }
        }

        string GetClassName(string tableName, string prefix)
        {
            string className = tableName;
            if (!string.IsNullOrEmpty(prefix) && tableName.Length > prefix.Length)
            {
                if (tableName.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
                    className = tableName.Substring(prefix.Length);
            }

            //Handling spaces & _ in table name
            return string.Join("", className.Split(new[] { ' ', '_' }, StringSplitOptions.RemoveEmptyEntries).Select(x => capitalize(x)).ToArray());
        }

        string GetMemberName(string colName, bool isNavigationProperty)
        {
            //Handling cases of ending with id and _id for navigation properties;
            var memberName = isNavigationProperty ? Regex.Replace(colName, "(?<name_root>.+)_?id$", "${name_root}", RegexOptions.IgnoreCase) : colName;

            //Handling cases of spaces, underscores in between;
            return string.Join("", memberName.Split(new[] { ' ', '_' }, StringSplitOptions.RemoveEmptyEntries).Select(x => capitalize(x)).ToArray());
        }

        List<Column> GetOrderedColumns(Table table)
        {
            var columns = new List<Column>();
            columns.AddRange(table.Columns.Where(c => c.IsPrimaryKey));
            columns.AddRange(table.Columns.Where(c => !c.IsPrimaryKey));
            return columns;
        }

        private void DeleteAssociation(AgileFx.AgileModeler.Association assoc)
        {
            var navProp1 = assoc.Source.NavigationProperties.Find(np => np.Name == assoc.End1NavigationProperty);
            if (navProp1 != null) navProp1.Delete();
            var navProp2 = assoc.Target.NavigationProperties.Find(np => np.Name == assoc.End2NavigationProperty);
            if (navProp2 != null) navProp2.Delete();
            assoc.Delete();
        }
    }
}
