using System.Net;
using Xunit.Abstractions;

namespace ActionsMinUtils.Tests;

public class GitHubTests : IDisposable
{
    private readonly ITestOutputHelper _output;
    
    public GitHubTests(ITestOutputHelper output)
    {
        this._output = output;
        
        // Change console output so we can capture it
        ConsoleOutput.Capture();

        // Instantiate a client with a delay of 0 seconds between retries
        TimeSpan Delay(int attempt)
        {
            return TimeSpan.FromSeconds(0);
        }

        GitHub = new GitHub("token", Delay);
    }

    public void Dispose()
    {
        ConsoleOutput.Release();
    }

    private GitHub GitHub { get; }

    [Fact]
    public async void ExecuteAsyncPassThru()
    {
        ValueTask<bool> Pass()
        {
            return ValueTask.FromResult(true);
        }

        Assert.True(await GitHub.ExecuteAsync(Pass!));
    }
    
    [Fact]
    public async void ExecuteAsyncRetries()
    {
        var e = new HttpRequestException("snap", null, HttpStatusCode.InternalServerError);
        Func<ValueTask<bool>> fail = () => throw e;

        // Validate the (last) exception is thrown
        await Assert.ThrowsAsync<HttpRequestException>(async () => await GitHub.ExecuteAsync(fail));

        // Validate the output has 5 warnings
        _output.WriteLine("-------");
        _output.WriteLine(ConsoleOutput.Output());
        _output.WriteLine("-------");
        var lines = ConsoleOutput.Output().Split(Environment.NewLine,
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ToList();
        _output.WriteLine("-------");
        _output.WriteLine(string.Join(Environment.NewLine, lines));
        _output.WriteLine("-------");
        
        Assert.Equal(5, lines.Count);
        Assert.Equal(5, lines.Where((line, i) => line.Contains($"attempt #{i + 1} of 5")).Count());
    }
}