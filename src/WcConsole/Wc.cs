namespace WcConsole;

public class Wc(IFiles files, TextWriter textWriter)
{
    public static string GetOutput(string str, string file) => $"{str} {file}";
    public FileMeta GetFileMeta(string path)
    {
        var file = new FileInfo(path);
        if (!files.Exists(file))
        {
            throw new InvalidOperationException("File does not exist");
        }

        return new FileMeta(file, path);
    }

    public void InvokeReadBytes(FileMeta meta)
    {
        var bytes = files.ReadAllBytes(meta.FileInfo.FullName).LongLength;
        textWriter.WriteLine(Wc.GetOutput($"{bytes}", meta.InputPath));
    }
}
