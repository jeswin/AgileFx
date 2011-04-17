using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Microsoft.VisualStudio.Modeling.Validation;
using AgileFx.AgileModeler.CustomCode.Validation;

namespace AgileFx.AgileModeler
{
    //Validation methods for ModelClass go here.
    [ValidationState(ValidationState.Enabled)]
    partial class ModelClass
    {
        [ValidationMethod (ValidationCategories.Open | ValidationCategories.Save | ValidationCategories.Menu)]
        // This method is applied to each instance of the ModelClass in a model. 
        public void ValidateUniqueName(ValidationContext context)
        {
            var error = Validator.ValidateUniqueName(this);
            if (error != null) error.Log(context);
        }


        [ValidationMethod(ValidationCategories.Open | ValidationCategories.Save | ValidationCategories.Menu)]
        private void ValidateUniqueMembers(ValidationContext context)
        {
            var errors = Validator.ValidateUniqueMembers(this);
            foreach (var error in errors) error.Log(context);
        }
    }
}