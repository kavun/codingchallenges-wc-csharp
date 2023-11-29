using System.Text;

namespace WcConsole;

public record Counts(ulong Lines, ulong Words, ulong Bytes, ulong Chars);

public static class WordCounter
{
    public static string GetResult(TextReader textReader, CountingOption[] options, string? fileName)
    {
        var counts = GetCounts(textReader);
        return GetResult(options, fileName, counts);
    }

    public static string GetResult(CountingOption[] options, string? fileName, Counts counts)
    {
        var result = new StringBuilder();
        foreach (var option in options)
        {
            result.Append(option switch
            {
                CountingOption.Default => $"{counts.Lines}\t{counts.Words}\t{counts.Bytes}\t",
                CountingOption.Lines => $"{counts.Lines}\t",
                CountingOption.Words => $"{counts.Words}\t",
                CountingOption.Bytes => $"{counts.Bytes}\t",
                CountingOption.Chars => $"{counts.Chars}\t",
                _ => throw new NotImplementedException()
            });
        }

        if (fileName != null)
        {
            result.Append(fileName);
        }

        return result.ToString();
    }

    public static Counts GetCounts(TextReader reader)
    {
        ulong words = 0;
        ulong lines = 0;
        ulong bytes = 0;
        ulong chars = 0;

        char? prevChar = null;
        int readChar;

        while ((readChar = reader.Read()) != -1)
        {
            char currentChar = (char)readChar;

            bytes += ByteCounter.GetUTF8ByteCount(currentChar);
            chars++;

            var isNewLine = currentChar == '\n';
            if (isNewLine)
            {
                lines += 1;
            }

            // char.IsWhiteSpace() includes a lot
            // https://learn.microsoft.com/en-us/dotnet/api/system.char.iswhitespace?view=net-8.0#remarks
            var isNewWord = !char.IsWhiteSpace(currentChar) && (!prevChar.HasValue || char.IsWhiteSpace(prevChar.Value));
            if (isNewWord)
            {
                words += 1;
            }

            prevChar = currentChar;
        }

        return new Counts(lines, words, bytes, chars);
    }
}
