using System.Text;

namespace WcConsole.Tests;
public class WcTests
{
    [Fact]
    public void GetResult_Tabbed()
    {
        var result = Wc.GetResult([WcOp.Lines, WcOp.Words, WcOp.Bytes, WcOp.Chars], null, new WCounts(0, 0, 0, 0));
        Assert.Equal("0\t0\t0\t0\t", result);
    }

    [Fact]
    public void GetResult_Tabbed_WithFileName()
    {
        var result = Wc.GetResult([WcOp.Lines, WcOp.Words, WcOp.Bytes, WcOp.Chars], "kavun", new WCounts(0, 0, 0, 0));
        Assert.Equal("0\t0\t0\t0\tkavun", result);
    }

    [Theory]
    [InlineData("../../../../../input/test.txt", 7145)]
    [InlineData("../../../../../input/test2.txt", 1)]
    public void GetCounts_Lines(string filePath, ulong expected)
    {
        using var reader = new StreamReader(filePath, Encoding.UTF8);
        var counts = Wc.GetCounts(reader);
        Assert.Equal(expected, counts.Lines);
    }

    [Theory]
    [InlineData("../../../../../input/test.txt", 58164)]
    [InlineData("../../../../../input/test2.txt", 2)]
    public void GetCounts_Words(string filePath, ulong expected)
    {
        using var reader = new StreamReader(filePath, Encoding.UTF8);
        var counts = Wc.GetCounts(reader);
        Assert.Equal(expected, counts.Words);
    }

    [Theory]
    [InlineData("../../../../../input/test.txt", 342190)]
    [InlineData("../../../../../input/test2.txt", 8)]
    public void GetCounts_Bytes(string filePath, ulong expected)
    {
        using var reader = new StreamReader(filePath, Encoding.UTF8);
        var counts = Wc.GetCounts(reader);
        Assert.Equal(expected, counts.Bytes);
    }

    [Theory]
    [InlineData("../../../../../input/test.txt", 342190)]
    [InlineData("../../../../../input/test2.txt", 8)]
    public void GetCounts_Chars(string filePath, ulong expected)
    {
        using var reader = new StreamReader(filePath, Encoding.UTF8);
        var counts = Wc.GetCounts(reader);
        Assert.Equal(expected, counts.Chars);
    }
}
