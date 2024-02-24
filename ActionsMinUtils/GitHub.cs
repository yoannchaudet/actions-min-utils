using System.Net;
using Octokit;
using Polly;
using Polly.Retry;
using Connection = Octokit.GraphQL.Connection;
using ProductHeaderValue = Octokit.ProductHeaderValue;

namespace ActionsMinUtils;

/// <summary>
///     GitHub API clients for both REST and GraphQL APIs with support for retry and a few convenience methods.
/// </summary>
public class GitHub(string githubToken, Func<int, TimeSpan>? delay = null)
{
    /// <summary>
    ///     Header to pass to each API calls
    /// </summary>
    private const string Header = "actions-min-utils";

    /// <summary>
    ///     REST client.
    /// </summary>
    public GitHubClient RestClient { get; } = new(new ProductHeaderValue(Header))
    {
        Credentials = new Credentials(githubToken)
    };

    /// <summary>
    ///     GraphQL client.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public Connection GraphQLClient { get; } = new(new Octokit.GraphQL.ProductHeaderValue(Header),
        githubToken);

    /// <summary>
    ///     Polly resilience pipeline for retrying API calls.
    /// </summary>
    private ResiliencePipeline ResiliencePipeline { get; } = new ResiliencePipelineBuilder().AddRetry(
        new RetryStrategyOptions
        {
            MaxRetryAttempts = 5,

            // Only handle 500 and 502 errors
            ShouldHandle = new PredicateBuilder()
                .Handle<HttpRequestException>(r =>
                    r.StatusCode == HttpStatusCode.InternalServerError || r.StatusCode == HttpStatusCode.BadGateway)
                .Handle<AuthorizationException>(),

            // Add a little randomness to the delay
            UseJitter = true,

            // Use a static delay reference (to which to add jitter)
            //  2 ^ 0 = 1 seconds then
            //  2 ^ 1 = 2 seconds then
            //  2 ^ 2 = 4 seconds then
            //  2 ^ 3 = 8 seconds then
            //  2 ^ 4 = 16 seconds
            DelayGenerator = args =>
                new ValueTask<TimeSpan?>(delay?.Invoke(args.AttemptNumber) ??
                                         TimeSpan.FromSeconds(Math.Pow(2, args.AttemptNumber))),

            // Logging (for each attempt)
            OnRetry = static args =>
            {
                Logger.Warning(
                    $"GitHub API call failed in {args.Duration.ToString()}, attempt #{args.AttemptNumber + 1} of 5, retrying in #{args.RetryDelay}): {args.Outcome.Exception!}");
                return default;
            }
        }).Build();

    /// <summary>
    ///     Execute a callback through a resilience pipeline that handles retry and logging.
    /// </summary>
    /// <param name="callback"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public async ValueTask<TResult> ExecuteAsync<TResult>(
        Func<ValueTask<TResult>> callback)
    {
        return await ResiliencePipeline.ExecuteAsync(async _ => await callback());
    }
}