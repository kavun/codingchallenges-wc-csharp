using WcConsole;

try
{
    using var _ = new ConsoleCanceller();

    var options = ArgParser.ParseOptions(args);
    var filePath = ArgParser.ParseFilePath(args);
    var textReader = TextSourcer.GetFileOrStdIn(filePath);
    var result = WordCounter.GetResult(textReader, options, filePath);

    Console.Out.WriteLine(result);
    return 0;
}
catch (Exception ex)
{
#if DEBUG
    Console.Error.WriteLine(ex);
#else
    Console.Error.WriteLine(ex.Message);
#endif
    return 1;
}
