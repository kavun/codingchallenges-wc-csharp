namespace WcConsole;

public class SingleLineWriter(TextWriter textWriter) : ICountWriter
{
    private const string Prepend = "  ";
    public void Write(long count)
    {
        textWriter.Write(Prepend);
        textWriter.Write(count.ToString());
    }
}
