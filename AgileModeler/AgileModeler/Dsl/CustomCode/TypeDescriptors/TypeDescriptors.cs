using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace AgileFx.AgileModeler.CustomCode.TypeDescriptors
{
    public abstract class TypeDescriptorBase : CustomTypeDescriptor
    {
        protected readonly object instance = null;
        protected readonly TypeDescriptionProvider parentTypeDescriptionProvider;
        public TypeDescriptorBase(object instance, TypeDescriptionProvider parent)
        {
            this.instance = instance;
            parentTypeDescriptionProvider = parent;
        }
    }

    public class ModeFieldTypeDescriptor : TypeDescriptorBase
    {
        public ModeFieldTypeDescriptor(object instance, TypeDescriptionProvider parent) : base(instance, parent) { }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection props =
                parentTypeDescriptionProvider.GetTypeDescriptor(instance).GetProperties();

            foreach (PropertyDescriptor p in props.OfType<PropertyDescriptor>())
            {
                switch (p.Name)
                {
                    case "IsFixedLength":
                    case "IsUnicode":
                    case "MaxLength":
                        if ((instance as ModelField).Type != BuiltInTypes.String) props.Remove(p);
                        break;
                }
            }

            return props;
        }
    }

    public class NavigationPropertyTypeDescriptor : TypeDescriptorBase
    {
        public NavigationPropertyTypeDescriptor(object instance, TypeDescriptionProvider parent) : base(instance, parent) { }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection props =
                parentTypeDescriptionProvider.GetTypeDescriptor(instance).GetProperties();

            try
            {
                var navProp = instance as NavigationProperty;
                var assoc = navProp.Store.ElementDirectory.FindElements<Association>().First(a => a.Name == navProp.Association);
                var primaryMultiplicities = new[] { Multiplicity.One, Multiplicity.ZeroOne };
                var propsToBeRemoved = new List<string>();

                if (!(primaryMultiplicities.Any(m => m == assoc.End1Multiplicity) && primaryMultiplicities.Any(m => m == assoc.End2Multiplicity)))
                {
                    propsToBeRemoved.AddRange(new[] { "IsForeignkey", "ForeignkeyColumn" });
                }
                else if (!navProp.IsForeignkey)
                {
                    propsToBeRemoved.Add("ForeignkeyColumn");
                }

                foreach (PropertyDescriptor p in props.OfType<PropertyDescriptor>())
                    if (propsToBeRemoved.Contains(p.Name)) props.Remove(p);
            }
            catch { }

            return props;
        }
    }

    public class AssociationTypeDescriptor : TypeDescriptorBase
    {
        public AssociationTypeDescriptor(object instance, TypeDescriptionProvider parent) : base(instance, parent) { }

        public override PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection props =
                parentTypeDescriptionProvider.GetTypeDescriptor(instance).GetProperties();

            var assoc = instance as Association;
            var propsToBeRemoved = new List<string>();

            if (!(assoc.End1Multiplicity == Multiplicity.ZeroMany && assoc.End2Multiplicity == Multiplicity.ZeroMany))
            {
                propsToBeRemoved.AddRange(new[] { "ManyToManyMappingTable", "End1ManyToManyMappingColumn", 
                    "End1ManyToManyNavigationProperty", "End1ManyToManyFieldName", "End2ManyToManyMappingColumn", 
                    "End2ManyToManyNavigationProperty", "End2ManyToManyFieldName" });
            }

            foreach (PropertyDescriptor p in props.OfType<PropertyDescriptor>())
                if (propsToBeRemoved.Contains(p.Name)) props.Remove(p);

            return props;
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }
    }
}
