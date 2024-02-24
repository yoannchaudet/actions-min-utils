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
var user = await github.RestClient.User.Current();
Logger.Info($"User {user.Email} (which is probably you) has ID {user.Id}!");

internal class Context : ActionContext
{
    public string GitHubWorkspace => GetEnvironmentVariable("GITHUB_WORKSPACE", true)!;
    public string GitHubToken => GetEnvironmentVariable("GITHUB_TOKEN", true)!;
    public string Name => GetInput("name", true)!;
}