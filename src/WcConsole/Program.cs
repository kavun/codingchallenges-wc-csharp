using WcConsole;

try
{
    using var cancellationTokenSource = new CancellationTokenSource();
    Console.CancelKeyPress += (s, e) =>
    {
        Console.WriteLine("Canceling...");
        cancellationTokenSource.Cancel();
        e.Cancel = true;
    };

    var passedOptions = ArgParser.ParseOp(args);

    string? inputPath = null;
    string? input = null;
    if (args.Length == 0)
    {
        input = await Console.In.ReadToEndAsync(cancellationTokenSource.Token);
    }
    else
    {
        var last = args.Last();
        if (!last.StartsWith('-'))
        {
            inputPath = last;
            var file = new FileInfo(last);
            if (!file.Exists)
            {
                Console.Error.WriteLine("File does not exist");
                return 1;
            }
            input = await File.ReadAllTextAsync(last, cancellationTokenSource.Token);
        }
        else
        {
            input = await Console.In.ReadToEndAsync(cancellationTokenSource.Token);
        }
    }

    if (input is null)
    {
        Console.Error.WriteLine("Invalid input");
        return 1;
    }

    var wc = new Wc(input, new SingleLineWriter(Console.Out));

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
                Console.Error.WriteLine("Invalid argument");
                return 1;
        }
    }

    if (inputPath is not null)
    {
        Console.WriteLine($" {inputPath}");
    }
    else
    {
        Console.WriteLine();
    }

    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex.Message);
    return 1;
}
