namespace ActionsMinUtils;

/// <summary>
///     A base class for reading environment variable and inputs from the context of a Docker Action.
/// </summary>
public abstract class ActionContext
{
    /// <summary>
    ///     Get an environment variable by name.
    /// </summary>
    /// <param name="variable"></param>
    /// <param name="required"></param>
    /// <returns></returns>
    public string? GetEnvironmentVariable(string variable, bool required = false)
    {
        if (variable == "")
            Logger.Fatal("Variable name cannot be empty");
        var value = GetSystemEnvironmentVariable(variable);
        if (value == null && required)
            Logger.Fatal($"Unable to read environment variable '{variable}' marked as required");
        return value;
    }

    /// <summary>
    ///     Get an input by name.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="required"></param>
    /// <returns></returns>
    public string? GetInput(string input, bool required = false)
    {
        if (input == "")
            Logger.Fatal("Input name cannot be empty");
        var value = GetSystemEnvironmentVariable($"INPUT_{input.ToUpper()}");
        if (value == null && required)
            Logger.Fatal($"Unable to read input '{input}' marked as required");
        return value;
    }

    // Indirection for testing purposes only
    internal virtual string? GetSystemEnvironmentVariable(string variable)
    {
        return Environment.GetEnvironmentVariable(variable);
    }
}