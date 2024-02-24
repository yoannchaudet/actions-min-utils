namespace ActionsMinUtils.Tests;

// Utility class for locking console output in parallel tests
public static class ConsoleOutput
{
    private static readonly Semaphore Lock = new(1, 1);

    private static StringWriter? _writer;

    public static void Capture()
    {
        Lock.WaitOne();
        Console.SetOut(_writer = new StringWriter());
    }

    public static string Output()
    {
        _writer!.Flush();
        return _writer!.ToString();
    }

    public static void Release()
    {
        Lock.Release();
    }
}