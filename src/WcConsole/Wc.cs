using System.Text;

namespace WcConsole;

public record WCounts(ulong Lines, ulong Words, ulong Bytes, ulong Chars);

public static class Wc
{
    public static string GetResult(TextReader textReader, WcOp[] ops, string? fileName)
    {
        var counts = GetCounts(textReader);
        return GetResult(ops, fileName, counts);
    }

    public static string GetResult(WcOp[] ops, string? fileName, WCounts counts)
    {
        var sb = new StringBuilder();
        foreach (var op in ops)
        {
            sb.Append(op switch
            {
                WcOp.Default => $"{counts.Lines}\t{counts.Words}\t{counts.Bytes}\t",
                WcOp.Lines => $"{counts.Lines}\t",
                WcOp.Words => $"{counts.Words}\t",
                WcOp.Bytes => $"{counts.Bytes}\t",
                WcOp.Chars => $"{counts.Chars}\t",
                _ => throw new NotImplementedException()
            });
        }

        if (fileName != null)
        {
            sb.Append(fileName);
        }

        return sb.ToString();
    }

    public static WCounts GetCounts(TextReader reader)
    {
        ulong words = 0;
        ulong lines = 0;
        ulong bytes = 0;
        ulong chars = 0;
        char? prev = null;
        int read = reader.Read();
        char[] chars1 = new char[1];
        while (read != -1)
        {
            char current = (char)read;

            bytes += (ulong)Encoding.UTF8.GetByteCount([current]);
            chars++;

            var isNewLine = current == '\n';
            if (isNewLine)
            {
                lines += 1;
            }

            // char.IsWhiteSpace() includes a lot
            // https://learn.microsoft.com/en-us/dotnet/api/system.char.iswhitespace?view=net-8.0#remarks
            var isNewWord = !char.IsWhiteSpace(current) && (!prev.HasValue || char.IsWhiteSpace(prev.Value));
            if (isNewWord)
            {
                words += 1;
            }

            prev = current;
            read = reader.Read();
        }

        return new WCounts(lines, words, bytes, chars);
    }
}
