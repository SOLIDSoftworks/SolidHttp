﻿$path = Split-Path $MyInvocation.MyCommand.Path

& "$path\build.ps1" `
 -buildRunner 'MyGet' `
 -nugetPath ''  `
 -msBuildPath "$($Env:ProgramFiles)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MsBuild.exe"  `
 -sourcesPath $path  `
 -configuration 'Release'  `
 -platform 'Any CPU'  `
 -targets 'Rebuild'  `
 -versionFormat '1.1.0-RC{0}'  `
 -buildCounter '1'  `
 -prereleaseTag 'RC0001'  `
 -packageVersion '1.0.0-RC0001' `
 -enableNugetPackageRestore 'true' `
 -gallioEcho ''  `
 -xunit192Path ''  `
 -xunit20Path ''  `
 -vsTestConsole ''  `
 -msTestExe ''  `
 -gitPath ''  `
 -gitVersion ''  `
 -testNamespaceFilter ''  `
 -verbose  