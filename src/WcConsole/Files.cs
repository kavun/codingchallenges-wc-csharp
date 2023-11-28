namespace WcConsole;
public class SystemFiles : IFiles
{
    public bool Exists(FileInfo file) => file.Exists;
    public byte[] ReadAllBytes(string path) => File.ReadAllBytes(path);
}

public interface IFiles
{
    bool Exists(FileInfo file);
    byte[] ReadAllBytes(string path);
}