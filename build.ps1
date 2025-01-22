$packagePath = Join-Path $PSScriptRoot "package"
$solutionPath = Join-Path $packagePath "Rdap.sln"
$testPath = Join-Path $packagePath "Test\RdapClient.Test\RdapClient.Test.csproj"
$rdapClientPath = Join-Path $packagePath "RdapClient\RdapClient.csproj"

dotnet restore $solutionPath
dotnet build $solutionPath --configuration Release --no-restore --no-incremental
dotnet test  $testPath --configuration Release --no-build
dotnet pack  $rdapClientPath --configuration Release --no-build