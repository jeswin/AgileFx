using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Validation;

namespace AgileFx.AgileModeler.CustomCode.Validation
{
    public class ValidationError
    {
        public string Code { get; private set; }
        public string Message { get; private set; }
        public ModelElement[] ErrorSources { get; private set; }

        public ValidationError(string message, string code, params ModelElement[] sources)
        {
            Message = message;
            Code = code;
            ErrorSources = sources;
        }

        public void Log(ValidationContext context)
        {
            context.LogError(Message, Code, ErrorSources);
        }
    }
}
