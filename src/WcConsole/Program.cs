using WcConsole;

try
{
    using var canceller = new ConsoleCanceller();
    var passedOptions = ArgParser.Parse(args);
    var response = await SourceMaterial.GetAsync(args, canceller.Token);
    if (response.IsError)
    {
        Console.Error.WriteLine(response.ErrorMessage);
        return 1;
    }

    var wc = new Wc(response.InputString, new SingleLineWriter(Console.Out));

    foreach (var op in passedOptions)
    {
        switch (op)
        {
            case WcOp.All:
                wc.InvokeAll();
                break;
            case WcOp.Bytes:
                wc.InvokeReadBytes();
                break;
            case WcOp.Lines:
                wc.InvokeReadLines();
                break;
            case WcOp.Words:
                wc.InvokeReadWords();
                break;
            case WcOp.Chars:
                wc.InvokeReadChars();
                break;
            default:
                Console.Error.WriteLine($"invalid option");
                return 1;
        }
    }

    if (response.FilePath is not null)
    {
        Console.WriteLine($" {response.FilePath}");
    }
    else
    {
        Console.WriteLine();
    }

    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex);
    return 1;
}
