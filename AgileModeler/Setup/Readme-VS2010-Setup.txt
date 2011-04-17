Steps to create a setup for AgileModeler for VS 2010

1. Run DslProjectsMigrationTool.exe on the VS 2008 projects
	- Choose output folder as VS2010-DSL-Migration (eg: C:\projects\AgileFx\AgileModeler\Setup\VS2010-DSL-Migration)
from C:\Program Files (x86)\Microsoft Visual Studio 2010 SDK\VisualStudioIntegration\Tools\DSLTools


2. In source.extension.tt in DslPackage, add "InstalledByMsi" after Description
....
<Description><#= this.Dsl.Description #></Description>
<InstalledByMsi>true</InstalledByMsi>
....

3. Follow steps in the VMSDK_Lab_5.pdf to create a Setup.

4. 


9686762313 Dinesh