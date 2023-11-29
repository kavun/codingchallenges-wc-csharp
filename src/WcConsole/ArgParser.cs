using System.Text.RegularExpressions;

namespace WcConsole;
public static partial class ArgParser
{
    public static WcOp[] ParseOp(string[] args)
    {
        var doubleDashArgs = new Dictionary<string, WcOp>
        {
            { "--bytes", WcOp.Bytes },
            { "c", WcOp.Bytes },
            { "--lines", WcOp.Lines },
            { "l", WcOp.Lines },
            { "--words", WcOp.Words },
            { "w", WcOp.Words},
            { "--chars", WcOp.Chars },
            { "m", WcOp.Chars }
        };

        var singleDashArgs = new Dictionary<char, WcOp>
        {
            { 'c', WcOp.Bytes },
            { 'l', WcOp.Lines },
            { 'w', WcOp.Words},
            { 'm', WcOp.Chars }
        };

        WcOp[] passedOptions =
        [
            .. args
                .Where(doubleDashArgs.ContainsKey)
                .Select(arg => doubleDashArgs[arg])
                .OrderBy(op => op),
            .. args
                .Where(opt => PosixArgRegex().IsMatch(opt))
                .SelectMany(arg => arg.TrimStart('-').ToCharArray())
                .Select(arg => singleDashArgs[arg])
                .OrderBy(op => op),
        ];

        passedOptions = [.. passedOptions.Distinct().OrderBy(op => op)];

        return passedOptions;
    }

    [GeneratedRegex("^\\-[^\\-]")]
    private static partial Regex PosixArgRegex();
}
