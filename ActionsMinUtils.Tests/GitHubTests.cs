using System.Net;

namespace ActionsMinUtils.Tests;

[CollectionDefinition("Console tests", DisableParallelization = true)]
public class GitHubTests : IDisposable
{
    public GitHubTests()
    {
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
        var lines = ConsoleOutput.Output().Split(Environment.NewLine,
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ToList();
        Assert.Equal(5, lines.Count);
        Assert.Equal(5, lines.Where((line, i) => line.Contains($"attempt #{i + 1} of 5")).Count());
    }
}