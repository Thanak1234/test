"C:\sonarscanner-msbuild\SonarScanner.MSBuild.exe" begin /k:"NW-K2" /v:%BUILD_SOURCEVERSION%
"C:\Program Files (x86)\MSBuild\14.0\bin\msbuild.exe" Workflow.sln /t:Rebuild
"C:\sonarscanner-msbuild\SonarScanner.MSBuild.exe" end
