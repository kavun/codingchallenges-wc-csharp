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
    ci ................. run github actions locally
    help, -? ........... show this help message
#>
param(
  [Parameter(Position=0)]
  [ValidateSet("run", "test", "testrun", "ci", "help")]
  [string]$Command,

  [Parameter(Position=1, ValueFromRemainingArguments=$true)]
  $Rest
)

function Invoke-Help { Get-Help $PSCommandPath }

if (!$Command) {
    Invoke-Help
    exit
}

function Invoke-Run([string]$Rest) {
    & dotnet run `
        --project $PSScriptRoot\src\WcConsole\WcConsole.csproj `
        -- $Rest
}

function Invoke-TestRun([string]$Rest) {
    @("-c", "-l", "-w", "-m", "-cmlw", "-cm -lw", "--lines -wm") | ForEach-Object {
        $wc = "wc $_ $PSScriptRoot\input\test.txt"
        $wc | Write-Host -F DarkGray
        Invoke-Expression $wc

        $ccwc = "dotnet run --project $PSScriptRoot\src\WcConsole\WcConsole.csproj -- $_ $PSScriptRoot\input\test.txt"
        $ccwc | Write-Host -F DarkGray
        Invoke-Expression $ccwc
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
    "ci" { Invoke-Ci }
    "help" { Invoke-Help }
}
