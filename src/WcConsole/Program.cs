using WcConsole;

try
{
    if (args.Length == 0)
    {
        Console.Error.WriteLine("No arguments provided");
        return 1;
    }

    var systemFiles = new SystemFiles();
    var wc = new Wc(systemFiles, Console.Out);

    if (args.Length == 2)
    {
        var file = wc.GetFileMeta(args[1]);

        switch (args[0])
        {
            case "--bytes":
            case "-c":
                wc.InvokeReadBytes(file);
                break;
            default:
                Console.Error.WriteLine("Invalid argument");
                return 1;
        }

        return 0;
    }

    Console.Error.WriteLine("Invalid number of arguments");
    return 1;
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex.Message);
    return 1;
}