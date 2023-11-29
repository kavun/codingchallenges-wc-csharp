using System.Text.RegularExpressions;

namespace WcConsole;

public static partial class ArgParser
{
    public static CountingOption[] ParseOptions(string[] args)
    {
        var doubleDashArgs = new Dictionary<string, CountingOption>
        {
            { "--lines", CountingOption.Lines },
            { "--words", CountingOption.Words },
            { "--bytes", CountingOption.Bytes },
            { "--chars", CountingOption.Chars },
        };

        var singleDashArgs = new Dictionary<char, CountingOption>
        {
            { 'l', CountingOption.Lines },
            { 'w', CountingOption.Words },
            { 'c', CountingOption.Bytes },
            { 'm', CountingOption.Chars }
        };

        CountingOption[] passedOptions =
        [
            .. args
                .Where(doubleDashArgs.ContainsKey)
                .Select(arg => doubleDashArgs[arg]),
            .. args
                .Where(opt => SingleDashArgRegex().IsMatch(opt))
                .SelectMany(arg => arg.TrimStart('-').ToCharArray())
                .Where(singleDashArgs.ContainsKey)
                .Select(arg => singleDashArgs[arg])
        ];

        passedOptions = [.. passedOptions.Distinct().OrderBy(op => op)];

        if (passedOptions.Length == 0)
        {
            passedOptions = [CountingOption.Default];
        }

        return passedOptions;
    }

    public static string? ParseFilePath(string[] args)
    {
        string? filePath = args.LastOrDefault();
        if (!string.IsNullOrWhiteSpace(filePath) && !filePath.StartsWith('-'))
        {
            return filePath;
        }

        return null;
    }

    [GeneratedRegex("^\\-[^\\-]")]
    private static partial Regex SingleDashArgRegex();
}
