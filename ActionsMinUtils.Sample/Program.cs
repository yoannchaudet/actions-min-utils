using ActionsMinUtils;

var ctx = new Context();
Logger.Debug($"Current workspace is {ctx.GitHubWorkspace}");
using (Logger.Group("Greetings"))
{
    Logger.Info("Hello world!");
}

internal class Context : ActionContext
{
    public string GitHubWorkspace => GetEnvironmentVariable("GITHUB_WORKSPACE", true)!;
}