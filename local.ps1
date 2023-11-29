<#
.SYNOPSIS
Local dev commands for WcCSharp

.DESCRIPTION
USAGE
    .\local.ps1 <command>

COMMANDS
    run ................ run
    test ............... run tests
    testrun ............ run with test input
    publish ............ publish .exe
    ci ................. run github actions locally
    help, -? ........... show this help message
#>
param(
  [Parameter(Position=0)]
  [ValidateSet("run", "test", "testrun", "publish", "ci", "help")]
  [string]$Command,

  [Parameter(Position=1, ValueFromRemainingArguments=$true)]
  $Rest
)

function Invoke-Help { Get-Help $PSCommandPath }

if (!$Command) {
    Invoke-Help
    exit
}

function Invoke-Publish {
    & dotnet publish -c Release -r win-x64 $PSScriptRoot\src\WcConsole\WcConsole.csproj
}

function Invoke-Run([string]$Rest) {
    iex "dotnet run --project $PSScriptRoot\src\WcConsole\WcConsole.csproj -- $Rest"
}

function Invoke-TestRun([string]$Rest) {
    Invoke-Publish

    @("", "-c", "-l", "-w", "-m", "-cmlw", "-cm -lw", "--lines -wm") | ForEach-Object {
        $wc = "wc $_ $PSScriptRoot\input\test.txt"
        $wc | Write-Host -F DarkGray
        Invoke-Expression $wc

        $ccwc = "$PSScriptRoot\src\WcConsole\bin\Release\net8.0\win-x64\publish\WcConsole.exe -- $_ $PSScriptRoot\input\test.txt"
        $ccwc | Write-Host -F DarkGray
        Invoke-Expression $ccwc

        $wc2 = "cat $PSScriptRoot\input\test.txt | wc $_"
        $wc2 | Write-Host -F DarkGray
        Invoke-Expression $wc2

        $ccwc2 = "cat $PSScriptRoot\input\test.txt | $PSScriptRoot\src\WcConsole\bin\Release\net8.0\win-x64\publish\WcConsole.exe $_"
        $ccwc2 | Write-Host -F DarkGray
        Invoke-Expression $ccwc2
    }
}

function Invoke-Test {
    & dotnet test $PSScriptRoot\WcCSharp.sln
}

function Invoke-Ci {
    & act
}

switch ($Command) {
    "run" { Invoke-Run "$Rest" }
    "test" { Invoke-Test }
    "testrun" { Invoke-TestRun "$Rest" }
    "publish" { Invoke-Publish }
    "ci" { Invoke-Ci }
    "help" { Invoke-Help }
}
