#region Using directives

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;

#endregion

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle(@"")]
[assembly: AssemblyDescription(@"")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(@"AgileFx")]
[assembly: AssemblyProduct(@"AgileModeler")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: System.Resources.NeutralResourcesLanguage("en")]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion(@"1.0.0.0")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: ReliabilityContract(Consistency.MayCorruptProcess, Cer.None)]

//
// Make the Dsl project internally visible to the DslPackage assembly
//
[assembly: InternalsVisibleTo(@"AgileFx.AgileModeler.DslPackage, PublicKey=00240000048000009400000006020000002400005253413100040000010001006DE9B005A95427CB1572405573DD1DDBADE9B09D772C8546CC4EF095E7F05E9902C4211BC627DA1954C0ECC9D5411DB110966AED6901D0345371080E49D9F04CC32B2878EDED3303E8F10ED660A1B2ABA405347C65586C9FF1366B5D488F2E0AE704CF5E8EDFB33CE81ADD8DAAD29ACBE72D9DDDA0FC570F11282635E4CEFE93")]