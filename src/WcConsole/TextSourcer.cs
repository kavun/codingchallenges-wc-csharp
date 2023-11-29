namespace WcConsole;

public static class TextSourcer
{
    public static TextReader GetFileOrStdIn(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return Console.In;
        }

        // this can throw if file does not exist, we'll catch in Program.cs
        return new StreamReader(filePath);
    }
}
