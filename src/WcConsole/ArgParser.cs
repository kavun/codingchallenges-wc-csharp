using System.Text.RegularExpressions;

namespace WcConsole;
public static partial class ArgParser
{
    public static WcOp[] Parse(string[] args)
    {
        var doubleDashArgs = new Dictionary<string, WcOp>
        {
            { "--lines", WcOp.Lines },
            { "--words", WcOp.Words },
            { "--bytes", WcOp.Bytes },
            { "--chars", WcOp.Chars },
        };

        var singleDashArgs = new Dictionary<char, WcOp>
        {
            { 'l', WcOp.Lines },
            { 'w', WcOp.Words},
            { 'c', WcOp.Bytes },
            { 'm', WcOp.Chars }
        };

        WcOp[] passedOptions =
        [
            .. args
                .Where(doubleDashArgs.ContainsKey)
                .Select(arg => doubleDashArgs[arg]),
            .. args
                .Where(opt => PosixArgRegex().IsMatch(opt))
                .SelectMany(arg => arg.TrimStart('-').ToCharArray())
                .Where(singleDashArgs.ContainsKey)
                .Select(arg => singleDashArgs[arg])
        ];

        passedOptions = [.. passedOptions.Distinct().OrderBy(op => op)];

        if (passedOptions.Length == 0 || passedOptions.Length == (Enum.GetValues<WcOp>().Length - 1))
        {
            passedOptions = [WcOp.Default];
        }

        return passedOptions;
    }

    [GeneratedRegex("^\\-[^\\-]")]
    private static partial Regex PosixArgRegex();
}
