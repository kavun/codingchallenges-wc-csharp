namespace WcConsole.Tests;
public class WcTests
{
    [Fact]
    public void GetOutput_ReturnsConcatenatedString()
    {
        // Act
        var actual = Wc.GetOutput("1", "2");

        // Assert
        Assert.Equal("1 2", actual);
    }

    [Fact]
    public void GetFileMeta_ThrowsInvalidOperationException_WhenFileDoesNotExist()
    {
        // Arrange
        var files = new TestFiles(false);
        var wc = new Wc(files, Console.Out);

        // Act
        var ex = Assert.Throws<InvalidOperationException>(() => wc.GetFileMeta("file"));

        // Assert
        Assert.Equal("File does not exist", ex.Message);
    }

    [Fact]
    public void GetFileMeta_ReturnsFileMeta_WhenFileExists()
    {
        // Arrange
        var files = new TestFiles(true);
        var wc = new Wc(files, Console.Out);

        // Act
        var actual = wc.GetFileMeta("file");

        // Assert
        Assert.Equal("file", actual.InputPath);
    }

    [Fact]
    public void InvokeReadBytes_WritesBytesToConsole()
    {
        // Arrange
        var file = new FileMeta(new FileInfo("file/name"), "file/name");
        var console = new StringWriter();
        var wc = new Wc(new TestFiles(true, []), console);
        var expected = $"0 file/name{Environment.NewLine}";

        // Act
        wc.InvokeReadBytes(file);

        // Assert
        Assert.Equal(expected, console.ToString());
    }

    public class TestFiles(bool exists, byte[]? bytes = default) : IFiles
    {
        public bool Exists(FileInfo file) => exists;

        public byte[] ReadAllBytes(string path)
        {
            return bytes ?? [];
        }
    }
}
