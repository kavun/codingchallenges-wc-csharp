namespace WcConsole.Tests;
public class WcTests
{
    private readonly StringWriter _fakeConsole;
    private readonly SingleLineWriter _lineWriter;

    public WcTests()
    {
        _fakeConsole = new StringWriter();
        _lineWriter = new SingleLineWriter(_fakeConsole);
    }

    [Fact]
    public void InvokeReadBytes()
    {
        var wc = new Wc("hello", _lineWriter);
        wc.InvokeReadBytes();
        Assert.Equal("  5", _fakeConsole.ToString());
    }

    [Fact]
    public void InvokeReadLines()
    {
        var wc = new Wc("hello", _lineWriter);
        wc.InvokeReadLines();
        Assert.Equal("  1", _fakeConsole.ToString());
    }

    [Fact]
    public void InvokeReadWords()
    {
        var wc = new Wc("hello world", _lineWriter);
        wc.InvokeReadWords();
        Assert.Equal("  2", _fakeConsole.ToString());
    }

    [Fact]
    public void InvokeReadChars()
    {
        var wc = new Wc("hello", _lineWriter);
        wc.InvokeReadChars();
        Assert.Equal("  5", _fakeConsole.ToString());
    }

    [Fact]
    public void InvokeAll()
    {
        var wc = new Wc("hello world", _lineWriter);
        wc.InvokeAll();
        Assert.Equal("  1  2  11", _fakeConsole.ToString());
    }
}
