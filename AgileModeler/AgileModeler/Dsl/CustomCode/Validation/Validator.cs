using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileFx.AgileModeler.CustomCode.Validation
{
    public static class Validator
    {
        public static ValidationError ValidateUniqueName(ModelClass modelClass)
        {
            var errors = new List<ValidationError>();
            var types = modelClass.Store.ElementDirectory.FindElements<ModelClass>().Where(c => c.Name == modelClass.Name).ToArray();
            if (types.Length > 1)
            {
                return new ValidationError("There are more than one classes with the name: '" + modelClass.Name + "'."
                    , "MC001UniqueNameError", types);
            }
            return null;
        }

        public static IEnumerable<ValidationError> ValidateUniqueMembers(ModelClass modelClass)
        {
            var errors = new List<ValidationError>();
            var dupeList = new List<string>();
            var members = getMembers(modelClass, true);
            members.ForEach(member =>
            {
                if (dupeList.Contains(member.Name)) return;
                var itemErrors = ValidateUniqueMember(member, modelClass);
                if (itemErrors.Count() > 0)
                {
                    dupeList.Add(member.Name);
                    errors.AddRange(itemErrors);
                }
            });
            return errors;
        }

        public static IEnumerable<ValidationError> ValidateUniqueMember(ModelFieldBase member, ModelClass modelClass)
        {
            var errors = new List<ValidationError>();
            var members = getMembers(modelClass, true);
            if (members.FindAll(f => (f != member) && (f.Name == member.Name)).Count > 0)
            {
                errors.Add(new ValidationError(string.Format("The name '{0}' is not unique in the heirarchy.", member.Name), "MC002UniqueMemberError", modelClass));
            }
            return errors;
        }

        private static List<ModelFieldBase> getMembers(ModelClass modelClass, bool includeInerited)
        {
            var members = new List<ModelFieldBase>();
            while (true)
            {
                members.AddRange(modelClass.Fields.Cast<ModelFieldBase>().Union(modelClass.NavigationProperties.Cast<ModelFieldBase>()));
                if (!includeInerited || modelClass.Baseclass == null) break;
                modelClass = modelClass.Baseclass; 
            }
            return members;
        }
    }
}
