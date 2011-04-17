using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.Modeling;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;
using AgileFx.AgileModeler.DslPackage.CustomCode;
using AgileFx.AgileModeler.DslPackage.CustomCode.Forms;
using AgileFx.AgileModeler.DslPackage.Lib;
using System.Windows.Forms;
using AgileFx.AgileModeler.DslPackage.CustomCode.MenuCommands;
using AgileFx.AgileModeler.CustomCode.Validation;
using Microsoft.VisualStudio.Modeling.Diagrams;
using AgileFx.AgileModeler.DslPackage.Utils;
using AgileFx.AgileModeler.DslPackage.CustomCode.DomainUtils;

namespace AgileFx.AgileModeler.DslPackage
{
    internal partial class AgileModelerDocData
    {
        private EventHandler<ElementAddedEventArgs> elementAddedEventHandler;
        private EventHandler<ElementDeletedEventArgs> elementDeletedEventHandler;
        private EventHandler<ElementPropertyChangedEventArgs> elementPropertyChangedEventHandler;

        protected override void OnDocumentLoaded()
        {
            base.OnDocumentLoaded();

            this.elementAddedEventHandler = this.HandleElementAdded;
            this.elementDeletedEventHandler = this.HandleElementDeleted;
            this.elementPropertyChangedEventHandler = this.HandleElementPropertyChanged;

            this.Store.EventManagerDirectory.ElementAdded.Add(this.elementAddedEventHandler);
            this.Store.EventManagerDirectory.ElementDeleted.Add(this.elementDeletedEventHandler);
            this.Store.EventManagerDirectory.ElementPropertyChanged.Add(this.elementPropertyChangedEventHandler);
            this.InitializeDiagram();
        }

        protected override void OnDocumentSaving(EventArgs e)
        {
            var diagram = GetCurrentDiagram(this.Store);
            var modelRoot = diagram.ModelElement as ModelRoot;
            if (string.IsNullOrEmpty(modelRoot.ConnectionString))
            {
                using (var tx = diagram.Store.TransactionManager.BeginTransaction("Initialize", false))
                {
                    modelRoot.ConnectionString = ConnectionUtil.GetDefaultConnectionString(diagram.Name);
                    if (tx.HasPendingChanges) tx.Commit();
                }
            }
            base.OnDocumentSaving(e);
        }

        private void InitializeDiagram()
        {
            var diagram = GetCurrentDiagram(this.Store);
            if (!diagram.Initialized)
            {
                // Display a form to the user. The form collects 
                // input for initializing the diagram.
                var dlg = new ItemDetailsForm();
                dlg.ShowDialog();
                var data = dlg.GetInitializeData();

                ModelerTransaction.Enter(() =>
                    {
                        using (var tx = diagram.Store.TransactionManager.BeginTransaction("Initialize", false))
                        {
                            (diagram.ModelElement as ModelRoot).DataContextName = data.ContextName;
                            diagram.Initialized = true;

                            if (tx.HasPendingChanges) tx.Commit();
                        }
                        if (data.ImportFromDatabase)
                        {
                            ConnectionUtil.SetExistingConnection(diagram.ModelElement as ModelRoot);
                            var sync = new Utilities.DbSchemaImporter(diagram);
                            sync.ImportModels();
                        }
                        else
                        {
                            ConnectionUtil.GetOrCreateConnectionString(diagram.ModelElement as ModelRoot, diagram.Name);
                        }
                    });

            }
        }

        public void HandleElementAdded(object sender, ElementAddedEventArgs e)
        {
            if (ModelerTransaction.IsInTransaction) return;
         
            ModelerTransaction.Enter(() =>
                {
                    using (Transaction tx = (sender as Store).TransactionManager.BeginTransaction())
                    {
                        if (e.ModelElement is ModelClass)
                        {
                            e.ModelElement.Delete();
                            var dlg = new AddEntityForm(sender as Store);
                            dlg.ShowDialog();
                        }
                        else if (e.ModelElement is Inheritance)
                        {
                            var inheritance = e.ModelElement as Inheritance;
                            var baseClassName = inheritance.Superclass.Name;
                            var derivedClassName = inheritance.Subclass.Name;
                            inheritance.Delete();
                            InheritanceUtil.AddInheritance(sender as Store, baseClassName, derivedClassName);
                        }
                        else if (e.ModelElement is Association)
                        {
                            var assoc = e.ModelElement as Association;
                            var dlg = new AddAssociationForm(sender as Store);
                            dlg.SetSelectedClasses(assoc.Source, assoc.Target);
                            assoc.Delete();
                            dlg.ShowDialog();
                        }
                        else if (e.ModelElement is NavigationProperty)
                        {
                            var parentClass = (e.ModelElement as NavigationProperty).ModelClass;
                            e.ModelElement.Delete();
                            var addAssociationForm = new AddAssociationForm(sender as Store);
                            addAssociationForm.SetSelectedClasses(parentClass);
                            addAssociationForm.ShowDialog();
                        }

                        if (tx.HasPendingChanges) tx.Commit();
                    }
                });
        }

        public void HandleElementDeleted(object sender, ElementDeletedEventArgs e)
        {
            if (ModelerTransaction.IsInTransaction) return;

            ModelerTransaction.Enter(() =>
                {
                    var store = sender as Store;
                    using (Transaction tx = (sender as Store).TransactionManager.BeginTransaction())
                    {
                        if (e.ModelElement is NavigationProperty)
                        {
                            var navProp = e.ModelElement as NavigationProperty;
                            var assoc = (sender as Store).ElementDirectory.FindElements<Association>().FirstOrDefault(a => a.Name == navProp.Association);
                            if (assoc != null && assoc.IsActive)
                            {
                                if (navProp.IsForeignkey)
                                {
                                    var modelClassName = (assoc.End1NavigationProperty == navProp.Name) ? assoc.End1RoleName : assoc.End2RoleName;
                                    var modelClass = (sender as Store).ElementDirectory.FindElements<ModelClass>().FirstOrDefault(c => c.Name == modelClassName);
                                    if (modelClass != null) deleteField(modelClass, navProp.ForeignkeyColumn);
                                }
                                DeleteAssociation(assoc);
                            }
                        }
                        else if (e.ModelElement is Association)
                        {
                            DeleteAssociation(e.ModelElement as Association);
                        }

                        if (tx.HasPendingChanges) tx.Commit();

                    }
                });
        }

        Action<ModelClass, string> deleteField = (modelClass, columnName) =>
        {
            var field = modelClass.Fields.Find(f => f.ColumnName == columnName);
            if (field != null) field.Delete();
        };

        private void DeleteAssociation(Association assoc)
        {
            var navProp1 = assoc.Source.NavigationProperties.Find(np => np.Name == assoc.End1NavigationProperty);
            if (navProp1 != null)
            {
                if (navProp1.IsForeignkey) deleteField(navProp1.ModelClass, navProp1.ForeignkeyColumn);
                navProp1.Delete();
            }
            var navProp2 = assoc.Target.NavigationProperties.Find(np => np.Name == assoc.End2NavigationProperty);
            if (navProp2 != null)
            {
                if (navProp2.IsForeignkey) deleteField(navProp2.ModelClass, navProp2.ForeignkeyColumn);
                navProp2.Delete();
            }
            assoc.Delete();
        }

        public void HandleElementPropertyChanged(object sender, ElementPropertyChangedEventArgs e)
        {
            if (ModelerTransaction.IsInTransaction || e.TransactionContext.ContextInfo.Count != 0
                || e.ModelElement.IsDeleted) return;

            ModelerTransaction.Enter(() =>
                {
                    using (Transaction tx = (sender as Store).TransactionManager.BeginTransaction())
                    {
                        if (e.ModelElement is ModelFieldBase)
                            (e.ModelElement as ModelFieldBase).IsEdited = true;
                        if (e.ModelElement is Association)
                            (e.ModelElement as Association).IsEdited = true;

                        if (e.ModelElement is ModelClass)
                        {
                            if (e.DomainProperty.Name == "Name")
                            {
                                (sender as Store).ElementDirectory.FindElements<Association>().Where(a => a.End1RoleName == e.OldValue.ToString())
                                    .ToList().ForEach(a =>
                                    {
                                        a.End1RoleName = e.NewValue.ToString();
                                        setPropertyTypeNName(sender as Store, a.End2RoleName, a.End2NavigationProperty, e.NewValue.ToString());
                                    });
                                (sender as Store).ElementDirectory.FindElements<Association>().Where(a => a.End2RoleName == e.OldValue.ToString())
                                    .ToList().ForEach(a =>
                                    {
                                        a.End2RoleName = e.NewValue.ToString();
                                        setPropertyTypeNName(sender as Store, a.End1RoleName, a.End1NavigationProperty, e.NewValue.ToString());
                                    });
                            }
                        }
                        else if (e.ModelElement is NavigationProperty)
                        {
                            var navProp = e.ModelElement as NavigationProperty;
                            var propName = (e.DomainProperty.Name == "Name") ? e.OldValue.ToString() : navProp.Name;
                            var association = (sender as Store).ElementDirectory.FindElements<Association>().First(a => a.Name == navProp.Association);
                            if (e.DomainProperty.Name == "IsForeignkey")
                            {
                                var otherEnd = ModelUtil.GetOtherEnd(navProp);
                                if (ValidateForeignkeyChange((bool)e.NewValue, navProp, otherEnd))
                                {
                                    if (navProp.IsForeignkey)
                                        SetForeignkey(navProp, otherEnd);
                                    else
                                        SetForeignkey(otherEnd, navProp);
                                }
                                else
                                {
                                    MessageBox.Show("Invalid entry");
                                    navProp.IsForeignkey = (bool)e.OldValue;
                                }
                            }
                            else if (e.DomainProperty.Name == "Name" || e.DomainProperty.Name == "Multiplicity")
                            {
                                if (propName == association.End1NavigationProperty)
                                {
                                    association.End1NavigationProperty = navProp.Name;
                                    association.End2Multiplicity = navProp.Multiplicity;
                                }
                                else if (propName == association.End2NavigationProperty)
                                {
                                    association.End2NavigationProperty = navProp.Name;
                                    association.End1Multiplicity = navProp.Multiplicity;
                                }
                                if (e.DomainProperty.Name == "Multiplicity")
                                    MakeChangesForMultiplicity(association, sender as Store);
                            }
                        }
                        else if (e.ModelElement is ModelField)
                        {
                            var field = e.ModelElement as ModelField;
                            if (new[] { "Type", "UpdateCheck", "MaxLength" }.Any(x => x == e.DomainProperty.Name))
                            {
                                var nonComparableTypes = new[] { BuiltInTypes.Binary, BuiltInTypes.Timestamp };
                                if ((nonComparableTypes.Any(t => t == field.Type) || (field.Type == BuiltInTypes.String && field.MaxLength == 0))
                                    && field.UpdateCheck != ConcurrencyCheckFrequency.Never)
                                {
                                    field.UpdateCheck = ConcurrencyCheckFrequency.Never;
                                }
                            }
                            else if (e.DomainProperty.Name == "IsPrimaryKey" && (bool)e.NewValue)
                            {
                                field.IsDbGenerated = true;
                                field.IsIdentity = true;
                                field.Nullable = false;
                            }
                        }
                        else if (e.ModelElement is Association)
                        {
                            var assoc = e.ModelElement as Association;
                            if (e.DomainProperty.Name.StartsWith("End1"))
                            {
                                var role1 = (sender as Store).ElementDirectory.FindElements<ModelClass>().First(c => c.Name == assoc.End1RoleName);
                                var navPropName = (e.DomainProperty.Name == "End1NavigationProperty") ? e.OldValue.ToString() : assoc.End1NavigationProperty;
                                var navProperty1 = role1.NavigationProperties.Find(np => np.Name == navPropName);
                                navProperty1.Name = assoc.End1NavigationProperty;
                                ModelUtil.GetOtherEnd(navProperty1).Multiplicity = assoc.End1Multiplicity;
                            }
                            else
                            {
                                var role2 = (sender as Store).ElementDirectory.FindElements<ModelClass>().First(c => c.Name == assoc.End2RoleName);
                                var navPropName = (e.DomainProperty.Name == "End2NavigationProperty") ? e.OldValue.ToString() : assoc.End2NavigationProperty;
                                var navProperty2 = role2.NavigationProperties.Find(np => np.Name == navPropName);
                                navProperty2.Name = assoc.End2NavigationProperty;
                                ModelUtil.GetOtherEnd(navProperty2).Multiplicity = assoc.End2Multiplicity;
                            }
                            if (e.DomainProperty.Name.EndsWith("Multiplicity"))
                                MakeChangesForMultiplicity(assoc, sender as Store);
                        }
                        if (tx.HasPendingChanges) tx.Commit();
                    }
                });

        }

        bool ValidateForeignkeyChange(bool newValue, NavigationProperty navProp, NavigationProperty otherEnd)
        {
            if (newValue)
                return (navProp.Multiplicity == Multiplicity.One || (navProp.Multiplicity == Multiplicity.ZeroOne && otherEnd.Multiplicity != Multiplicity.One));
            else
                return (otherEnd.Multiplicity == Multiplicity.One || (otherEnd.Multiplicity == Multiplicity.ZeroOne && navProp.Multiplicity != Multiplicity.One));
        }

        private void setPropertyTypeNName(Store store, string className, string propertyName, string relatedType)
        {
            var classes = store.ElementDirectory.FindElements<ModelClass>().ToList();
            var cls = classes.Find(c => c.Name == className);
            var navProp = cls.NavigationProperties.Find(np => np.Name == propertyName);
            navProp.Type = (navProp.Multiplicity == Multiplicity.ZeroMany) ? "ICollection<" + relatedType + ">" : relatedType;
            navProp.Name = "";
            navProp.Name = ModelUtil.GetMemberName(relatedType, cls, navProp.Multiplicity == Multiplicity.ZeroMany);
            var assoc = store.ElementDirectory.FindElements<Association>().First(a => a.Name == navProp.Association);
            if (assoc.End1RoleName == className)
                assoc.End1NavigationProperty = navProp.Name;
            else
                assoc.End2NavigationProperty = navProp.Name;
        }

        void SetForeignkey(NavigationProperty foreignkeyProp, NavigationProperty otherEnd)
        {
            otherEnd.IsForeignkey = false;
            var otherEndForeignkeyColumn = otherEnd.ModelClass.Fields.Find(f => f.Name == otherEnd.ForeignkeyColumn);
            if (otherEndForeignkeyColumn != null) otherEndForeignkeyColumn.Delete();
            otherEnd.ForeignkeyColumn = null;

            foreignkeyProp.IsForeignkey = true;
            foreignkeyProp.ForeignkeyColumn = ModelUtil.GetMemberName(otherEnd.ModelClass.Name + "Id", foreignkeyProp.ModelClass, false);
            var otherEndPrimaryKeyField = otherEnd.ModelClass.Fields.Find(f => f.IsPrimaryKey);
            var newForeignkeyField = new ModelField(foreignkeyProp.Store) { Name = foreignkeyProp.ForeignkeyColumn };
            CopyAttributesFrom(otherEndPrimaryKeyField, newForeignkeyField);
            foreignkeyProp.ModelClass.Fields.Add(newForeignkeyField);
        }

        void CopyAttributesFrom(ModelField source, ModelField target)
        {
            target.DefaultValue = source.DefaultValue;
            target.IsFixedLength = source.IsFixedLength;
            target.IsUnicode = source.IsUnicode;
            target.MaxLength = source.MaxLength;
            target.Nullable = source.Nullable;
            target.Type = source.Type;
        }

        void MakeChangesForMultiplicity(Association assoc, Store store)
        {
            var models = store.ElementDirectory.FindElements<ModelClass>().ToList();

            var end1NavProp = models.Find(c => c.Name == assoc.End1RoleName).NavigationProperties.Find(np => np.Association == assoc.Name);
            var end2NavProp = models.Find(c => c.Name == assoc.End2RoleName).NavigationProperties.Find(np => np.Association == assoc.Name);
            setPropertyTypeNName(store, end1NavProp.ModelClass.Name, end1NavProp.Name, assoc.End2RoleName);
            setPropertyTypeNName(store, end2NavProp.ModelClass.Name, end2NavProp.Name, assoc.End1RoleName);

            if (end1NavProp.Multiplicity == Multiplicity.ZeroMany && end2NavProp.Multiplicity != Multiplicity.ZeroMany)
                if (!end1NavProp.IsForeignkey) SetForeignkey(end1NavProp, end2NavProp);
                else if (end2NavProp.Multiplicity == Multiplicity.ZeroMany && end1NavProp.Multiplicity != Multiplicity.ZeroMany)
                    if (!end2NavProp.IsForeignkey) SetForeignkey(end2NavProp, end1NavProp);

            if (assoc.End1Multiplicity == Multiplicity.ZeroMany && assoc.End2Multiplicity == Multiplicity.ZeroMany)
            {
                assoc.ManyToManyMappingTable = string.Format("{0}{1}Map", assoc.End1RoleName, assoc.End2RoleName);

                assoc.End1ManyToManyMappingColumn = assoc.End1RoleName;
                assoc.End1ManyToManyNavigationProperty = assoc.End1RoleName;
                assoc.End1ManyToManyFieldName = assoc.End1RoleName + "Id";

                assoc.End2ManyToManyMappingColumn = assoc.End2RoleName;
                assoc.End2ManyToManyNavigationProperty = assoc.End2RoleName;
                assoc.End2ManyToManyFieldName = assoc.End2RoleName + "Id";
            }
            else
            {
                assoc.ManyToManyMappingTable = null;
                assoc.End1ManyToManyMappingColumn = null;
                assoc.End1ManyToManyNavigationProperty = null;
                assoc.End1ManyToManyFieldName = null;
                assoc.End2ManyToManyMappingColumn = null;
                assoc.End2ManyToManyNavigationProperty = null;
                assoc.End2ManyToManyFieldName = null;
            }
            var diagram = GetCurrentDiagram(store);
            AgileFx.AgileModeler.CustomCode.DiagramUtil.AutoLayout(new List<ShapeElement>(new[] { diagram.FindShape(assoc) }), diagram);
        }

        private ClassDiagram GetCurrentDiagram(Store store)
        {
            var modelRoot = store.ElementDirectory.FindElements<ModelRoot>().Single();
            global::System.Collections.Generic.IList<DslDiagrams::PresentationElement> diagrams = DslDiagrams::PresentationViewsSubject.GetPresentation(modelRoot);
            if (diagrams.Count > 0)
                return diagrams[0] as global::AgileFx.AgileModeler.ClassDiagram;
            else
                return null;
        }
    }
}
