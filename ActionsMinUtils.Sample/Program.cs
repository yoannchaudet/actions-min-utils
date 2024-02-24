using ActionsMinUtils;

// Get stuff from the context
var ctx = new Context();

// Log a few things out of the context
Logger.Debug($"Current workspace is {ctx.GitHubWorkspace}");
Logger.Info($"Hello, {ctx.Name}!");
using (Logger.Group("Greetings"))
{
    Logger.Info("Hello world!");
}

// Make some API calls too
var github = new GitHub(ctx.GitHubToken);
var ownerName = ctx.GitHubActionRepository.Split('/')[0];
var repoName = ctx.GitHubActionRepository.Split('/')[1];
var repo = await github.ExecuteAsync(async () => await github.RestClient.Repository.Get(ownerName, repoName));
Logger.Info($"This action ran from repository {repo.Id} with License: {repo.License.Name}!");

internal class Context : ActionContext
{
    public string GitHubWorkspace => GetEnvironmentVariable("GITHUB_WORKSPACE", true)!;
    public string GitHubToken => GetEnvironmentVariable("GITHUB_TOKEN", true)!;
    public string GitHubActionRepository => GetEnvironmentVariable("GITHUB_ACTION_REPOSITORY", true)!;
    public string Name => GetInput("name", true)!;
}