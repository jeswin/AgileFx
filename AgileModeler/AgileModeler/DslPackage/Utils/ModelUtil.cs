using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgileFx.AgileModeler.DslPackage.Lib;

namespace AgileFx.AgileModeler.DslPackage.Utils
{
    public static class ModelUtil
    {
        public static NavigationProperty GetOtherEnd(NavigationProperty navProp)
        {
            var assoc = navProp.Store.ElementDirectory.FindElements<Association>().First(a => a.Name == navProp.Association);
            var relatedEntity = (assoc.End1RoleName == navProp.ModelClass.Name && assoc.End1NavigationProperty == navProp.Name) ? assoc.End2RoleName : assoc.End1RoleName;
            return navProp.Store.ElementDirectory.FindElements<ModelClass>().First(c => c.Name == relatedEntity).NavigationProperties.Find(np => np.Association == navProp.Association);
        }

        public static string GetMemberName(string name, ModelClass cls, bool isPlural)
        {
            Func<ModelClass, string, bool> memberExists = (modelClass, membername) => modelClass.NavigationProperties.Exists(p => p.Name == membername) || modelClass.Fields.Exists(field => field.Name == membername);
            Func<ModelClass, string, bool> memberExistsInTree = 
                (modelClass, memberName) => (modelClass.Name == memberName || memberExists(modelClass, memberName)) 
                    ? true : ((modelClass.Baseclass != null) ? memberExists(modelClass.Baseclass, memberName) : false);

            var keyword = isPlural ? AgileFx.AgileModeler.DslPackage.Lib.Pluralizer.ToPlural(name) : name;
            var ctr = 1;
            var tempName = keyword;
            while (memberExistsInTree(cls, tempName))
                tempName = keyword + (++ctr).ToString();
            return tempName;
        }
    }
}
