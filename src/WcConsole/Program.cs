using WcConsole;

try
{
    using var canceller = new ConsoleCanceller();

    var passedOptions = ArgParser.Parse(args);

    var response = SourceMaterial.Get(args);
    if (response.InputString is null)
    {
        Console.Error.WriteLine("no input");
        return 1;
    }

    var result = Wc.GetResult(response.InputString, passedOptions, response.FilePath);

    Console.WriteLine(result);

    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex);
    return 1;
}
