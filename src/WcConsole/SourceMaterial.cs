namespace WcConsole;
public record SourceMaterialResponse(string InputString, string? FilePath, bool IsError = false, string? ErrorMessage = default);
public static class SourceMaterial
{
    public static async Task<SourceMaterialResponse> GetAsync(string[] args, CancellationToken cancellationToken)
    {
        string? inputPath = null;
        var lastArg = args.Last();
        string? input;
        if (!lastArg.StartsWith('-'))
        {
            inputPath = lastArg;
            var file = new FileInfo(lastArg);
            if (!file.Exists)
            {
                return new SourceMaterialResponse(string.Empty, inputPath, true, "File does not exist");
            }
            input = await File.ReadAllTextAsync(lastArg, cancellationToken);
        }
        else
        {
            input = await Console.In.ReadToEndAsync(cancellationToken);
        }

        input ??= string.Empty;

        return new SourceMaterialResponse(input, inputPath);
    }
}
