namespace WcConsole;

public class ConsoleCanceller : IDisposable
{
    public ConsoleCanceller()
    {
        Console.CancelKeyPress += (_, e) =>
        {
            Console.WriteLine();
            _cts.Cancel();
            e.Cancel = true;
        };
    }

    private readonly CancellationTokenSource _cts = new();
    public CancellationToken Token => _cts.Token;
    public void Cancel() => _cts.Cancel();
    void IDisposable.Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
        GC.SuppressFinalize(this);
    }
}
