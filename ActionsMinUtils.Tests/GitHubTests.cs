using System.Net;
using Octokit;
using ActionsMinUtils.github;
using ActionsMinUtils.Tests.utils;
using Xunit.Abstractions;

namespace ActionsMinUtils.Tests;

public class GitHubTests : ConsoleOutputTests
{
    public GitHubTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        // Instantiate a client with a delay of 0 seconds between retries
        TimeSpan Delay(int attempt)
        {
            return TimeSpan.FromSeconds(0);
        }

        GitHub = new GitHub("token", Delay);
    }

    private GitHub GitHub { get; }

    [Fact]
    public async Task ExecuteAsyncPassThru()
    {
        ValueTask<bool> Pass()
        {
            return ValueTask.FromResult(true);
        }

        Assert.True(await GitHub.ExecuteAsync(Pass!));
    }

    [Fact]
    public async Task ExecuteAsyncRetries()
    {
        var e = new HttpRequestException("snap", null, HttpStatusCode.InternalServerError);
        Func<ValueTask<bool>> fail = () => throw e;

        // Validate the (last) exception is thrown
        await Assert.ThrowsAsync<HttpRequestException>(async () => await GitHub.ExecuteAsync(fail));

        // Validate the output has 5 warnings
        TestOutputHelper.WriteLine("-------");
        TestOutputHelper.WriteLine(ConsoleOutput.Output());
        TestOutputHelper.WriteLine("-------");
        var lines = ConsoleOutput.Output().Split(Environment.NewLine,
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ToList();
        TestOutputHelper.WriteLine("-------");
        TestOutputHelper.WriteLine(string.Join(Environment.NewLine, lines));
        TestOutputHelper.WriteLine("-------");

        Assert.Equal(5, lines.Count);
        Assert.Equal(5, lines.Where((line, i) => line.Contains($"attempt #{i + 1} of 5")).Count());
    }

    [Fact]
    public async Task ExecuteAsyncRetriesApiException()
    {
        var e = new ApiException("snap", HttpStatusCode.InternalServerError);
        Func<ValueTask<bool>> fail = () => throw e;

        // Validate the (last) exception is thrown
        await Assert.ThrowsAsync<ApiException>(async () => await GitHub.ExecuteAsync(fail));

        // Validate the output has 5 warnings
        TestOutputHelper.WriteLine("-------");
        TestOutputHelper.WriteLine(ConsoleOutput.Output());
        TestOutputHelper.WriteLine("-------");
        var lines = ConsoleOutput.Output().Split(Environment.NewLine,
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ToList();
        TestOutputHelper.WriteLine("-------");
        TestOutputHelper.WriteLine(string.Join(Environment.NewLine, lines));
        TestOutputHelper.WriteLine("-------");

        Assert.Equal(5, lines.Count);
        Assert.Equal(5, lines.Where((line, i) => line.Contains($"attempt #{i + 1} of 5")).Count());
    }

    [Fact]
    public void VerifyInheritance()
    {
        Assert.True(typeof(AuthorizationException).IsSubclassOf(typeof(ApiException)), "AuthorizationException should be a subclass of ApiException");
    }
}