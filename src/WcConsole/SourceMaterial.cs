namespace WcConsole;

public record SourceMaterialResponse(
    TextReader? InputString,
    string? FilePath);

public static class SourceMaterial
{
    public static SourceMaterialResponse Get(string[] args)
    {
        string? inputPath = null;
        var lastArg = args.Length > 0 ? args.Last() : string.Empty;
        TextReader? input;
        if (!string.IsNullOrWhiteSpace(lastArg) && !lastArg.StartsWith('-'))
        {
            inputPath = lastArg;
            var file = new FileInfo(lastArg);
            if (!file.Exists)
            {
                return new SourceMaterialResponse(null, inputPath);
            }
            input = new StreamReader(file.FullName);
        }
        else
        {
            input = Console.In;
        }

        return new SourceMaterialResponse(input, inputPath);
    }
}
