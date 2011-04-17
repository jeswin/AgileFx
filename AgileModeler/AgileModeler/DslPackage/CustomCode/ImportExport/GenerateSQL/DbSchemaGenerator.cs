using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.Modeling.Diagrams;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.ImportExport.GenerateSQL
{
    public class DbSchemaGenerator
    {
        private Diagram diagram = null;
        public DbSchemaGenerator(Diagram diagram)
        {
            this.diagram = diagram;
        }

        public bool CleanUpDbSchema { get; set; }
        public bool UseNavigationPropertyNameForFKeys { get; set; }

        public StringBuilder GenerateScripts()
        {
            var sb = new StringBuilder();

            var models = diagram.Store.ElementDirectory.FindElements<ModelClass>().ToList();
            var associations = diagram.Store.ElementDirectory.FindElements<Association>().ToList();
            var inheritances = diagram.Store.ElementDirectory.FindElements<Inheritance>().ToList();


            Func<ModelClass, string> getTableName = cls => CleanUpDbSchema ? cls.Name : cls.TableName;
            Func<ModelField, string> getColumnName = fld => CleanUpDbSchema ? fld.Name : fld.ColumnName;


            Func<string, string, NavigationProperty> getNavProp = (roleName, navPropName) => models.Find(c => c.Name == roleName).NavigationProperties.Find(np => np.Name == navPropName);

            Func<ModelField, Multiplicity, FieldTypeInfo> getForeignkeyField = (fld, multiplicity) =>
            {
                var fldInfo = FieldTypeInfo.Parse(fld);
                fldInfo.IsIdentity = false;
                fldInfo.Nullable = (multiplicity == Multiplicity.ZeroOne);
                return fldInfo;
            };

            Func<string, ModelField> getSinglePrimarykeyField = className => GetRootBaseClass(models.Find(c => c.Name == className)).Fields.Single(f => f.IsPrimaryKey);

            Func<NavigationProperty, FieldTypeInfo> createForeignkeyField = navProp =>
            {
                var assoc = associations.Find(a => a.Name == navProp.Association);
                var relatedEntity = (assoc.End1RoleName == navProp.ModelClass.Name) ? assoc.End2RoleName : assoc.End1RoleName;
                var relatedPrimaryKeyField = getSinglePrimarykeyField(relatedEntity);
                return getForeignkeyField(relatedPrimaryKeyField, navProp.Multiplicity);
            };

            Func<ModelClass, string> getPrimaryKeyColumnName = model => (model.Baseclass != null) ? inheritances.Find(inh => inh.Subclass == model).DerivedClassPrimaryKeyColumn : getColumnName(model.Fields.Single(f => f.IsPrimaryKey));

            var connectionString = (diagram.ModelElement as ModelRoot).ConnectionString;
            var databaseName = string.IsNullOrEmpty(connectionString) ? "Unnamed Database" : GetDatabaseName(connectionString);

            //Start with use database
            sb.AppendLine(string.Format("USE [{0}]", databaseName));
            sb.AppendLine("GO");
            sb.AppendLine();

            //First.. Model Tables 
            models.ForEach(cls =>
            {
                sb.AppendLine(string.Format("CREATE TABLE [dbo].[{0}](", getTableName(cls)));
                
                if (cls.Baseclass != null)
                {
                    sb.AppendLine(string.Format("	[{0}] {1},", inheritances.Find(inh => inh.Subclass == cls).DerivedClassPrimaryKeyColumn, GetSQLType(FieldTypeInfo.Parse(getSinglePrimarykeyField(cls.Name)), true)));                    
                }
                foreach (var field in GetFilteredNOrderedFields(cls))
                {
                    sb.AppendLine(string.Format("	[{0}] {1},", getColumnName(field), GetSQLType(FieldTypeInfo.Parse(field))));
                }
                foreach (var np in cls.NavigationProperties)
                {
                    var foreignKeyColumn = UseNavigationPropertyNameForFKeys ? np.Name : np.ForeignkeyColumn;
                    if (np.IsForeignkey)
                        sb.AppendLine(string.Format("	[{0}] {1},", foreignKeyColumn, GetSQLType(createForeignkeyField(np))));
                }
                if (cls.Fields.Any(f => f.IsPrimaryKey) || cls.Baseclass != null)
                {
                    sb.AppendLine(string.Format(" CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED (", cls.Name));
                    if (cls.Baseclass != null)
                        sb.AppendLine(string.Format("	[{0}] ASC", inheritances.Find(inh => inh.Subclass == cls).DerivedClassPrimaryKeyColumn));
                    else
                        sb.AppendLine(string.Join("\n", cls.Fields.Where(f => f.IsPrimaryKey).Select(f => string.Format("	[{0}] ASC", getColumnName(f))).ToArray()));
                    sb.AppendLine(") ON [PRIMARY]");
                }
                sb.AppendLine(") ON [PRIMARY]");
                sb.AppendLine();
                sb.AppendLine("GO");
                sb.AppendLine();
            });

            var manyToManyAssociations = associations.Where(a => a.End1Multiplicity == Multiplicity.ZeroMany && a.End2Multiplicity == Multiplicity.ZeroMany).ToList();

            //Second... Mapping Tables
            manyToManyAssociations.ForEach(m2m =>
            {
                sb.AppendLine(string.Format("CREATE TABLE [dbo].[{0}](", m2m.ManyToManyMappingTable));
                sb.AppendLine(string.Format("	[{0}] {1},", m2m.End1ManyToManyMappingColumn, GetSQLType(getForeignkeyField(getSinglePrimarykeyField(m2m.End1RoleName), Multiplicity.ZeroMany))));
                sb.AppendLine(string.Format("	[{0}] {1},", m2m.End2ManyToManyMappingColumn, GetSQLType(getForeignkeyField(getSinglePrimarykeyField(m2m.End2RoleName), Multiplicity.ZeroMany))));

                //Primarykeys
                sb.AppendLine(string.Format(" CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED (", m2m.ManyToManyMappingTable));
                sb.AppendLine(string.Format("	[{0}] ASC,", m2m.End1ManyToManyMappingColumn));
                sb.AppendLine(string.Format("	[{0}] ASC", m2m.End2ManyToManyMappingColumn));
                sb.AppendLine(") ON [PRIMARY]");

                sb.AppendLine(") ON [PRIMARY]");
                sb.AppendLine();
                sb.AppendLine("GO");
                sb.AppendLine();
            });

            Func<string, string, ModelClass> getRelatedModel = (assoc, modelClass) =>
            {
                var association = associations.Find(a => a.Name == assoc);
                return models.Find(c => c.Name == ((association.End1RoleName == modelClass) ? association.End2RoleName : association.End1RoleName));
            };

            //Add relationships for inheritances
            inheritances.ForEach(inh =>
            {
                var foreignKeyName = string.Format("FK_{0}_{1}_{2}_{3}", getTableName(inh.Subclass), inh.DerivedClassPrimaryKeyColumn, getTableName(inh.Superclass), getPrimaryKeyColumnName(inh.Superclass));
                sb.AppendLine(string.Format("ALTER TABLE [dbo].[{0}]  WITH CHECK ADD  CONSTRAINT [{1}] FOREIGN KEY([{2}])", getTableName(inh.Subclass), foreignKeyName, inh.DerivedClassPrimaryKeyColumn));
                sb.AppendLine(string.Format("REFERENCES [dbo].[{0}] ([{1}])", getTableName(inh.Superclass), getPrimaryKeyColumnName(inh.Superclass)));
                sb.AppendLine("ON DELETE CASCADE");
                sb.AppendLine("GO");
                sb.AppendLine();
                sb.AppendLine(string.Format("ALTER TABLE [dbo].[{0}] CHECK CONSTRAINT [{1}]", getTableName(inh.Subclass), foreignKeyName));
                sb.AppendLine("GO");
                sb.AppendLine();
            });

            //Now all the relationships
            associations.ForEach(assoc =>
            {
                if (assoc.End1Multiplicity == Multiplicity.ZeroMany && assoc.End2Multiplicity == Multiplicity.ZeroMany)
                {
                    var end1Entity = models.Find(c => c.Name == assoc.End1RoleName);
                    var end2Entity = models.Find(c => c.Name == assoc.End2RoleName);

                    var end1ForeignKeyName = string.Format("FK_{0}_{1}_{2}_{3}", assoc.ManyToManyMappingTable, assoc.End1ManyToManyMappingColumn, getTableName(end1Entity), assoc.End1NavigationProperty);
                    sb.AppendLine(string.Format("ALTER TABLE [dbo].[{0}]  WITH CHECK ADD  CONSTRAINT [{1}] FOREIGN KEY([{2}])", assoc.ManyToManyMappingTable, end1ForeignKeyName, assoc.End1ManyToManyMappingColumn));
                    sb.AppendLine(string.Format("REFERENCES [dbo].[{0}] ([{1}])", getTableName(end1Entity), getPrimaryKeyColumnName(end1Entity)));
                    sb.AppendLine("ON DELETE CASCADE");
                    sb.AppendLine("GO");
                    sb.AppendLine();
                    sb.AppendLine(string.Format("ALTER TABLE [dbo].[{0}] CHECK CONSTRAINT [{1}]", assoc.ManyToManyMappingTable, end1ForeignKeyName));
                    sb.AppendLine("GO");
                    sb.AppendLine();

                    var end2ForeignKeyName = string.Format("FK_{0}_{1}_{2}_{3}", assoc.ManyToManyMappingTable, assoc.End2ManyToManyMappingColumn, getTableName(end2Entity), assoc.End2NavigationProperty);
                    sb.AppendLine(string.Format("ALTER TABLE [dbo].[{0}]  WITH CHECK ADD  CONSTRAINT [{1}] FOREIGN KEY([{2}])", assoc.ManyToManyMappingTable, end2ForeignKeyName, assoc.End2ManyToManyMappingColumn));
                    sb.AppendLine(string.Format("REFERENCES [dbo].[{0}] ([{1}])", getTableName(end2Entity), getPrimaryKeyColumnName(end2Entity)));
                    sb.AppendLine("ON DELETE CASCADE");
                    sb.AppendLine("GO");
                    sb.AppendLine();
                    sb.AppendLine(string.Format("ALTER TABLE [dbo].[{0}] CHECK CONSTRAINT [{1}]", assoc.ManyToManyMappingTable, end2ForeignKeyName));
                    sb.AppendLine("GO");
                    sb.AppendLine();
                }
                else
                {
                    var end1NavProp = getNavProp(assoc.End1RoleName, assoc.End1NavigationProperty);
                    var end2NavProp = getNavProp(assoc.End2RoleName, assoc.End2NavigationProperty);

                    if (end1NavProp.IsForeignkey)
                    {
                        var foreignKeyColumn = UseNavigationPropertyNameForFKeys ? end1NavProp.Name : (string.IsNullOrEmpty(end1NavProp.ForeignkeyColumn) ? end1NavProp.Name : end1NavProp.ForeignkeyColumn);
                        var foreignkeyName = CleanUpDbSchema ? string.Format("FK_{0}_{1}_{2}_{3}", getTableName(end1NavProp.ModelClass), end1NavProp.Name, getTableName(end2NavProp.ModelClass), end2NavProp.Name) : assoc.Name;
                        if (end2NavProp.Multiplicity != Multiplicity.ZeroMany) foreignkeyName += string.Format("_{0}To{1}", end2NavProp.Multiplicity, end1NavProp.Multiplicity);
                        sb.AppendLine(string.Format("ALTER TABLE [dbo].[{0}]  WITH CHECK ADD  CONSTRAINT [{1}] FOREIGN KEY([{2}])", getTableName(end1NavProp.ModelClass), foreignkeyName, foreignKeyColumn));
                        sb.AppendLine(string.Format("REFERENCES [dbo].[{0}] ([{1}])", getTableName(end2NavProp.ModelClass), getPrimaryKeyColumnName(end2NavProp.ModelClass)));
                        sb.AppendLine("GO");
                        sb.AppendLine();
                        sb.AppendLine(string.Format("ALTER TABLE [dbo].[{0}] CHECK CONSTRAINT [{1}]", getTableName(end1NavProp.ModelClass), foreignkeyName));
                        sb.AppendLine("GO");
                        sb.AppendLine();
                    }
                    else if (end2NavProp.IsForeignkey)
                    {
                        var foreignKeyColumn = UseNavigationPropertyNameForFKeys ? end2NavProp.Name : (string.IsNullOrEmpty(end2NavProp.ForeignkeyColumn) ? end2NavProp.Name : end2NavProp.ForeignkeyColumn);
                        var foreignkeyName = CleanUpDbSchema ? string.Format("FK_{0}_{1}_{2}_{3}", getTableName(end2NavProp.ModelClass), end2NavProp.Name, getTableName(end1NavProp.ModelClass), end1NavProp.Name) : assoc.Name;
                        if (end1NavProp.Multiplicity != Multiplicity.ZeroMany) foreignkeyName += string.Format("_{0}To{1}", end1NavProp.Multiplicity, end2NavProp.Multiplicity);
                        sb.AppendLine(string.Format("ALTER TABLE [dbo].[{0}]  WITH CHECK ADD  CONSTRAINT [{1}] FOREIGN KEY([{2}])", getTableName(end2NavProp.ModelClass), foreignkeyName, foreignKeyColumn));
                        sb.AppendLine(string.Format("REFERENCES [dbo].[{0}] ([{1}])", getTableName(end1NavProp.ModelClass), getPrimaryKeyColumnName(end1NavProp.ModelClass)));
                        sb.AppendLine("GO");
                        sb.AppendLine();
                        sb.AppendLine(string.Format("ALTER TABLE [dbo].[{0}] CHECK CONSTRAINT [{1}]", getTableName(end2NavProp.ModelClass), foreignkeyName));
                        sb.AppendLine("GO");
                        sb.AppendLine();
                    }
                }
            });

            return sb;
        }

        ModelClass GetRootBaseClass(ModelClass model)
        {
            return (model.Baseclass != null) ? GetRootBaseClass(model.Baseclass) : model;
        }

        List<ModelField> GetFilteredNOrderedFields(ModelClass model)
        {
            var foreignKeyColumns = model.NavigationProperties.Where(np => np.IsForeignkey)
                .Select(np => np.ForeignkeyColumn).ToList();
            var fieldSet = new List<ModelField>();
            fieldSet.AddRange(model.Fields.Where(f => f.IsPrimaryKey));
            fieldSet.AddRange(model.Fields.Where(f => !f.IsPrimaryKey).Where(f => !foreignKeyColumns.Contains(f.ColumnName)));
            return fieldSet;
        }

        string GetSQLType(FieldTypeInfo field)
        {
            return GetSQLType(field, false);
        }

        string GetSQLType(FieldTypeInfo field, bool ignoreIdentity)
        {
            string type = "";
            if (field.Type == BuiltInTypes.Boolean) type = "Bit";
            else if (field.Type == BuiltInTypes.Binary) type = "Binary";
            else if (field.Type == BuiltInTypes.Byte) type = "TinyInt";
            else if (field.Type == BuiltInTypes.DateTime) type = "DateTime";
            else if (field.Type == BuiltInTypes.Decimal) type = "Decimal";
            else if (field.Type == BuiltInTypes.Double) type = "Float";
            else if (field.Type == BuiltInTypes.Guid) type = "UniqueIdentifier";
            else if (field.Type == BuiltInTypes.Int16) type = "SmallInt";
            else if (field.Type == BuiltInTypes.Int32) type = "Int";
            else if (field.Type == BuiltInTypes.Int64) type = "BigInt";
            else if (field.Type == BuiltInTypes.Single) type = "Real";
            else if (field.Type == BuiltInTypes.String)
            {
                if (field.IsUnicode) type = "N";
                if (field.MaxLength <= 0)
                {
                    type += "Text";
                }
                else
                {
                    type += (field.IsFixedLength ? "Char" : "VarChar");
                    type = string.Format("{0}({1})", type, field.MaxLength);
                }

            }
            else if (field.Type == BuiltInTypes.Timestamp) type = "Timestamp";
            else throw new NotImplementedException("Mapping for SQL Type not implemented.");

            if (!ignoreIdentity && field.IsIdentity) type += " IDENTITY(1,1)";
            if (!field.Nullable) type += " NOT NULL";

            return type;
        }

        string GetDatabaseName(string connectionString)
        {
            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder();
            builder.ConnectionString = connectionString;
            return builder.InitialCatalog;
        }

        class FieldTypeInfo
        {
            public BuiltInTypes Type { get; set; }
            public bool IsUnicode { get; set; }
            public bool IsFixedLength { get; set; }
            public bool IsIdentity { get; set; }
            public bool Nullable { get; set; }
            public int MaxLength { get; set; }

            public static FieldTypeInfo Parse(ModelField field)
            {
                return new FieldTypeInfo
                {
                    Type = field.Type,
                    IsUnicode = field.IsUnicode,
                    IsFixedLength = field.IsFixedLength,
                    IsIdentity = field.IsIdentity,
                    Nullable = field.Nullable,
                    MaxLength = field.MaxLength
                };
            }
        }
    }
}
