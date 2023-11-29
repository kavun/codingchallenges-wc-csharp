namespace WcConsole.Tests;
public class ArgParserTests
{
    [Fact]
    public void ParseOp_Returns()
    {
        var actual = ArgParser.Parse(["--bytes"]);
        Assert.Equal([WcOp.Bytes], actual);
    }

    [Fact]
    public void ParseOp_ReturnsSorted()
    {
        var actual = ArgParser.Parse(["--bytes", "--lines"]);
        Assert.Equal([WcOp.Lines, WcOp.Bytes], actual);
    }

    [Fact]
    public void ParseOp_ReturnsMixed()
    {
        var actual = ArgParser.Parse(["--bytes", "--lines", "-w"]);
        Assert.Equal([WcOp.Lines, WcOp.Words, WcOp.Bytes], actual);
    }

    [Fact]
    public void ParseOp_FiltersDuplicates()
    {
        var actual = ArgParser.Parse(["--bytes", "--lines", "-w", "-w", "-l"]);
        Assert.Equal([WcOp.Lines, WcOp.Words, WcOp.Bytes], actual);
    }

    [Fact]
    public void ParseOp_DefaultsToAll()
    {
        var actual = ArgParser.Parse([]);
        Assert.Equal([WcOp.All], actual);
    }

    [Fact]
    public void ParseOp_ReturnsAll()
    {
        var actual = ArgParser.Parse(["-w", "-m", "-l", "-c"]);
        Assert.Equal([WcOp.All], actual);
    }

    [Fact]
    public void ParseOp_ReturnsSmashed()
    {
        var actual = ArgParser.Parse(["-cl"]);
        Assert.Equal([WcOp.Lines, WcOp.Bytes], actual);
    }
}
