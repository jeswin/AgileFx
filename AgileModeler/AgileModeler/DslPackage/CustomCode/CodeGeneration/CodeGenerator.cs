using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using AgileFx.AgileModeler.DslPackage.CustomCode.CodeGeneration;
using System.IO;
using System.Diagnostics;

namespace AgileFx.AgileModeler
{
     //<summary>
     //Declaration of the package providing the Simulation code generator
     //</summary>
#if VS_2008
    [ProvideCodeGenerator(typeof(AgileModelerCodeGenerator), "AgileModelerCodeGenerator",
    "Generates C# implementation of DomainModels described in .models files", true)]
#endif
#if VS_2010
    [ProvideCodeGenerator(typeof(AgileModelerCodeGeneratorVS2010), "AgileModelerCodeGeneratorVS2010",
    "Generates C# implementation of DomainModels described in .models files", true)]
#endif
    internal sealed partial class AgileModelerPackage
    {
    }

#if VS_2008 
    [global::System.Runtime.InteropServices.Guid("9799A402-F0A3-48ee-B103-2223E74553A4")]
    public class AgileModelerCodeGenerator : TemplatedCodeGenerator
#endif
#if VS_2010
    [global::System.Runtime.InteropServices.Guid("4A7F616F-2E28-4863-A6DA-997A95003A63")]
    public class AgileModelerCodeGeneratorVS2010 : TemplatedCodeGenerator
#endif
    {
        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            return GenerateCodeForTemplate(CodeGenerationResources.CodegenTemplate, inputFileName);
        }

        private byte[] GenerateCodeForTemplate(byte[] template, string inputFileName)
        {
            string templateFileContent = ASCIIEncoding.UTF8.GetString(template);

            const string ModelFileNameMarker = "%MODEL_FILENAME%";

            Debug.Assert(templateFileContent.Contains(ModelFileNameMarker),
                "Error - the template code does not contain the expected model file name marker");

            templateFileContent = templateFileContent.Replace(ModelFileNameMarker, inputFileName);
            return base.GenerateCode(inputFileName, templateFileContent);
        }
    }
}

