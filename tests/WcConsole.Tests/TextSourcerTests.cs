namespace WcConsole.Tests;

public class TextSourcerTests
{
    [Fact]
    public void GetFileOrStdIn_Null_ConsoleIn()
    {
        using var result = TextSourcer.GetFileOrStdIn(null);
        Assert.Equal(Console.In, result);
    }

    [Fact]
    public void GetFileOrStdIn_Empty_ConsoleIn()
    {
        using var result = TextSourcer.GetFileOrStdIn(string.Empty);
        Assert.Equal(Console.In, result);
    }

    [Fact]
    public void GetFileOrStdIn_WhiteSpace_ConsoleIn()
    {
        using var result = TextSourcer.GetFileOrStdIn(" ");
        Assert.Equal(Console.In, result);
    }

    [Fact]
    public void GetFileOrStdIn_WithGoodFilePath_StreamReader()
    {
        using var result = TextSourcer.GetFileOrStdIn("../../../../../input/test.txt");
        Assert.IsType<StreamReader>(result);
    }

    [Fact]
    public void GetFileOrStdIn_WithBogusFilePath_Throws()
    {
        Assert.Throws<FileNotFoundException>(() => TextSourcer.GetFileOrStdIn("kavun"));
    }
}
