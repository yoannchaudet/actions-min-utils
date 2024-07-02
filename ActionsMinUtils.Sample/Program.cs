using ActionsMinUtils;
using ActionsMinUtils.github;

// Get stuff from the context
var ctx = new Context();

// Log a few things out of the context
Logger.Debug($"Current workspace is {ctx.GitHubWorkspace}");
Logger.Info($"Hello, {ctx.Name}!");
using (Logger.Group("Here is a little greetings group"))
{
    Logger.Info("Hello world!");
}

// Make some API calls too
var github = new GitHub(ctx.GitHubToken);
var ownerName = ctx.GitHubRepository.Split('/')[0];
var repoName = ctx.GitHubRepository.Split('/')[1];
var repo = await github.ExecuteAsync(async () => await github.RestClient.Repository.Get(ownerName, repoName));
Logger.Info($"This action ran from the context of repository {repo.Id} and the {repo.License.Name}!");

internal class Context : ActionContext
{
    public string GitHubWorkspace => GetEnvironmentVariable(DefaultVariables.GitHubWorkspace, true)!;
    public string GitHubToken => GetEnvironmentVariable("GITHUB_TOKEN", true)!;
    public string GitHubRepository => GetEnvironmentVariable(DefaultVariables.GitHubRepository, true)!;
    public string Name => GetInput("name", true)!;
}