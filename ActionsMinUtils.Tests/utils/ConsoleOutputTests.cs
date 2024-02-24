using Xunit.Abstractions;

namespace ActionsMinUtils.Tests.utils;

// Base class for tests that need to capture console output
public abstract class ConsoleOutputTests : IDisposable
{
    protected ConsoleOutputTests(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;

        // Capture the output
        ConsoleOutput.Capture();
    }

    // A test output helper to help debug outputs as needed
    protected ITestOutputHelper TestOutputHelper { init; get; }

    public void Dispose()
    {
        // Release the output
        ConsoleOutput.Release();
    }
}