using System.Text;

namespace WcConsole;
public static class ByteCounter
{
    private static readonly char[] _charHolder = new char[1];
    public static ulong GetUTF8ByteCount(char c)
    {
        _charHolder[0] = c;
        return (ulong)Encoding.UTF8.GetByteCount(_charHolder);
    }
}
