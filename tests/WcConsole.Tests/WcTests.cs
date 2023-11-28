namespace WcConsole.Tests;
public class WcTests
{
    [Fact]
    public void Add_WhenCalled_ReturnsSumOfArguments()
    {
        // Arrange
        int a = 1;
        int b = 2;
        int expected = 3;

        // Act
        int actual = Wc.Add(a, b);

        // Assert
        Assert.Equal(expected, actual);
    }
}
