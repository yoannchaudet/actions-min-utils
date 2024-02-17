using System.Collections.ObjectModel;
using System.Text;

namespace ActionsMinUtils;

/// <summary>
/// A simple logger for GitHub Actions
/// </summary>
public static class Logger
{
    /// <summary>
    /// Write a debug message
    /// </summary>
    /// <param name="message"></param>
    public static void Debug(string? message)
    {
        IssueCommand(new CommandEnvelope("debug", null, message));
    }

    /// <summary>
    /// Write an error message (potentially along with an annotation)
    /// </summary>
    /// <param name="message"></param>
    /// <param name="annotationProps"></param>
    /// <param name="throws">If true, throws an exception</param>
    /// <returns></returns>
    public static Exception? Error(string? message, AnnotationProperties? annotationProps = null, bool throws = false)
    {
        IssueCommand(new CommandEnvelope("error", annotationProps.ToDictionary(), message));
        return throws ? new Exception(message) : null;
    }

    /// <summary>
    /// Write a warning message (potentially along with an annotation)
    /// </summary>
    /// <param name="message"></param>
    /// <param name="annotationProps"></param>
    public static void Warning(string? message, AnnotationProperties? annotationProps = null)
    {
        IssueCommand(new CommandEnvelope("warning", annotationProps.ToDictionary(), message));
    }

    /// <summary>
    /// Write a notice message (potentially along with an annotation)
    /// </summary>
    /// <param name="message"></param>
    /// <param name="annotationProps"></param>
    public static void Notice(string? message, AnnotationProperties? annotationProps = null)
    {
        IssueCommand(new CommandEnvelope("notice", annotationProps.ToDictionary(), message));
    }

    /// <summary>
    /// Write an info message
    /// </summary>
    /// <param name="message"></param>
    public static void Info(string message)
    {
        Console.WriteLine(message);
    }

    /// <summary>
    /// Write a group and return an IDisposable to be used in a using statement (for closing the group)
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IDisposable Group(string name)
    {
        IssueCommand(new CommandEnvelope("group", new Dictionary<string, string> { { "name", name } }, ""));
        return new EndGroupDisposable();
    }

    /// <summary>
    /// Issue a command on the standard output
    /// </summary>
    /// <param name="command"></param>
    private static void IssueCommand(CommandEnvelope command)
    {
        Console.WriteLine(command.ToString());
    }

    // Convert an annotation properties object to a dictionary
    private static Dictionary<string, string> ToDictionary(this AnnotationProperties? annotationProps)
    {
        return annotationProps == null
            ? new Dictionary<string, string>()
            : new Dictionary<string, string>
            {
                { "title", annotationProps.Title ?? "" },
                { "file", annotationProps.File ?? "" },
                { "line", annotationProps.StartLine ?? "" },
                { "endLine", annotationProps.EndLine ?? "" },
                { "col", annotationProps.StartColumn ?? "" },
                { "endColumn", annotationProps.EndColumn ?? "" }
            };
    }

    // Disposable responsible for closing a group
    private class EndGroupDisposable : IDisposable
    {
        public void Dispose()
        {
            IssueCommand(new CommandEnvelope("endgroup", null, ""));
        }
    }

    /// <summary>
    /// Annotation properties object
    /// </summary>
    public class AnnotationProperties
    {
        public string? Title { get; init; }
        public string? File { get; init; }
        public string? StartLine { get; init; }
        public string? EndLine { get; init; }
        public string? StartColumn { get; init; }
        public string? EndColumn { get; init; }
    }

    // Envelope objects for console commands
    public class CommandEnvelope(string command, Dictionary<string, string>? parameters, string? message)
    {
        private string Command { get; } = command;
        private ReadOnlyDictionary<string, string>? Parameters { get; } = parameters?.AsReadOnly();
        private string? Message { get; } = message;

        // Output the envelope as a string
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"::{Command}");
            if (Parameters?.Count > 0)
            {
                builder.Append(' ');
                builder.AppendJoin(",", Parameters.Select(p => $"{EscapeProperty(p.Key)}={EscapeProperty(p.Value)}"));
            }

            builder.Append("::");
            builder.Append(EscapeData(Message));
            return builder.ToString().TrimEnd();
        }

        private static string EscapeData(string? value)
        {
            return value == null
                ? ""
                : value
                    .Replace("%", "%25")
                    .Replace("\r", "%0D")
                    .Replace("\n", "%0A");
        }

        private static string EscapeProperty(string? value)
        {
            return value == null
                ? ""
                : value
                    .Replace("%", "%25")
                    .Replace("\r", "%0D")
                    .Replace("\n", "%0A")
                    .Replace(":", "%3A")
                    .Replace(",", "%2C");
        }
    }
}