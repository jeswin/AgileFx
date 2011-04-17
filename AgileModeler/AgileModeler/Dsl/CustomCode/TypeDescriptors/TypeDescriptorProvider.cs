using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.VisualStudio.Modeling;

namespace AgileFx.AgileModeler.CustomCode.TypeDescriptors
{
    public class TypeDescriptorProvider : TypeDescriptionProvider
    {        
        private readonly TypeDescriptionProvider parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRestrictionProvider"/> class.
        /// </summary>
        /// <param name="parent">The parent type description provider.</param>
        public TypeDescriptorProvider(TypeDescriptionProvider parent)
            : base(parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// Gets a custom type descriptor for the given type and object.
        /// </summary>
        /// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
        /// <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor"/>.</param>
        /// <returns>
        /// An <see cref="T:System.ComponentModel.ICustomTypeDescriptor"/> that can provide metadata for the type.
        /// </returns>
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            if (instance != null)
            {
                if (objectType == typeof(ModelField))
                {
                    return new ModeFieldTypeDescriptor(instance, parent);
                } else if (objectType == typeof(NavigationProperty))
                {
                    return new NavigationPropertyTypeDescriptor(instance, parent);
                }
                else if (objectType == typeof(Association))
                {
                    return new AssociationTypeDescriptor(instance, parent);
                }
            }
               
            return parent.GetTypeDescriptor(objectType, instance);
        }

        public static void RegisterTypes()
        {
            foreach (Type t in new[] { typeof(ModelField), typeof(NavigationProperty), typeof(Association) })
                TypeDescriptor.AddProvider(new TypeDescriptorProvider(TypeDescriptor.GetProvider(t)), t);
        }
    }
}
