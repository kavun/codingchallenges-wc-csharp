namespace WcConsole.Tests;

public class ArgParserTests
{
    [Fact]
    public void ParseOptions_Returns()
    {
        var actual = ArgParser.ParseOptions(["--bytes"]);
        Assert.Equal([CountingOption.Bytes], actual);
    }

    [Fact]
    public void ParseOptions_ReturnsSorted()
    {
        var actual = ArgParser.ParseOptions(["--bytes", "--lines"]);
        Assert.Equal([CountingOption.Lines, CountingOption.Bytes], actual);
    }

    [Fact]
    public void ParseOptions_ReturnsMixed()
    {
        var actual = ArgParser.ParseOptions(["--bytes", "--lines", "-w"]);
        Assert.Equal([CountingOption.Lines, CountingOption.Words, CountingOption.Bytes], actual);
    }

    [Fact]
    public void ParseOptions_FiltersDuplicates()
    {
        var actual = ArgParser.ParseOptions(["--bytes", "--lines", "-w", "-w", "-l"]);
        Assert.Equal([CountingOption.Lines, CountingOption.Words, CountingOption.Bytes], actual);
    }

    [Fact]
    public void ParseOptions_Default()
    {
        var actual = ArgParser.ParseOptions([]);
        Assert.Equal([CountingOption.Default], actual);
    }

    [Fact]
    public void ParseOptions_ReturnsAll()
    {
        var actual = ArgParser.ParseOptions(["-w", "-m", "-l", "-c"]);
        Assert.Equal([CountingOption.Lines, CountingOption.Words, CountingOption.Bytes, CountingOption.Chars], actual);
    }

    [Fact]
    public void ParseOptions_ReturnsSmashed()
    {
        var actual = ArgParser.ParseOptions(["-cl"]);
        Assert.Equal([CountingOption.Lines, CountingOption.Bytes], actual);
    }

    [Fact]
    public void ParseOptions_ReturnsSmashedWithMixed()
    {
        var actual = ArgParser.ParseOptions(["-cl", "-w"]);
        Assert.Equal([CountingOption.Lines, CountingOption.Words, CountingOption.Bytes], actual);
    }

    [Fact]
    public void ParseOptions_ReturnsSmashedWithMixedAndDoubleDash()
    {
        var actual = ArgParser.ParseOptions(["-cl", "-w", "--lines"]);
        Assert.Equal([CountingOption.Lines, CountingOption.Words, CountingOption.Bytes], actual);
    }

    [Fact]
    public void ParseFilePath_WithPath_Returns()
    {
        var actual = ArgParser.ParseFilePath(["--bytes", "kavun"]);
        Assert.Equal("kavun", actual);
    }

    [Fact]
    public void ParseFilePath_EndingDoubleDash_ReturnsNull()
    {
        var actual = ArgParser.ParseFilePath(["--bytes"]);
        Assert.Null(actual);
    }

    [Fact]
    public void ParseFilePath_EndingSingleDash_ReturnsNull()
    {
        var actual = ArgParser.ParseFilePath(["-w"]);
        Assert.Null(actual);
    }
}
