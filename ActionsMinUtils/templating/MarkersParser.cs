using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ActionsMinUtils.templating;

/// <summary>
///     A utility for creating and parsing markers in arbitrary text files.
///     Important: a marker should be fully contained on a single line.
/// </summary>
public class MarkersParser
{
    /// <summary>
    ///     Create a new marker parser.
    /// </summary>
    /// <param name="markerPrefix">
    ///     The prefix to use for the marker. Default to
    ///     <c>&lt;!--</c>
    /// </param>
    /// <param name="markerSuffix">The suffix to use for the marker. Default to <c>--></c></param>
    public MarkersParser(string markerPrefix = "<!--", string markerSuffix = "-->")
    {
        MarkerPrefix = markerPrefix;
        MarkerSuffix = markerSuffix;
        MarkerPattern = new Regex(
            $@"{Regex.Escape(MarkerPrefix)}\s*(?<name>[^\s]+)(\s+(?<args>[^\s]+))?\s*{Regex.Escape(MarkerSuffix)}",
            RegexOptions.Compiled);
    }

    public string MarkerPrefix { get; init; }
    public string MarkerSuffix { get; init; }

    private Regex MarkerPattern { get; }

    /// <summary>
    ///     Convert a marker to its string representation.
    /// </summary>
    /// <param name="marker"></param>
    /// <returns></returns>
    public string ToText(Marker marker)
    {
        var builder = new StringBuilder();

        // Prefix
        if (!string.IsNullOrWhiteSpace(MarkerPrefix))
            builder.Append($"{MarkerPrefix} ");

        // Name
        builder.Append(marker.Name);

        // Arguments
        if (marker.Arguments.Any())
        {
            builder.Append(" ");
            builder.Append(string.Join("&",
                marker.Arguments.Select(a => $"{a.Key}={HttpUtility.UrlEncode(a.Value)}")));
        }

        // Suffix
        if (!string.IsNullOrWhiteSpace(MarkerSuffix))
            builder.Append($" {MarkerSuffix}");
        return builder.ToString();
    }

    /// <summary>
    ///     Parse markers from a stream (that may not fit in memory).
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public IEnumerable<MarkerSpan> FromStream(StreamReader reader)
    {
        using (reader)
        {
            var lineNumber = 1;
            var index = 0;
            while (reader.ReadLine() is { } line)
            {
                foreach (var m in MarkerPattern.Matches(line).AsEnumerable())
                {
                    var name = m.Groups["name"].Value;
                    var args = HttpUtility.ParseQueryString(m.Groups["args"].Value);
                    var arguments = new Dictionary<string, string>();
                    foreach (string? o in args) arguments.Add(o!, args[o!]!);
                    yield return new MarkerSpan(name, arguments, index + m.Index, m.Length, lineNumber);
                }

                // Increment counters
                lineNumber++;
                index += line.Length + Environment.NewLine.Length;
            }
        }
    }

    /// <summary>
    ///     Parse markers from a string.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public IEnumerable<MarkerSpan> FromText(string text)
    {
        var reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(text)));
        return FromStream(reader);
    }

    /// <summary>
    ///     Return the content between two markers in a given text.
    /// </summary>
    /// <param name="text">The text in which to return the content.</param>
    /// <param name="start">Start marker.</param>
    /// <param name="end">End marker.</param>
    /// <param name="content">The content between the markers.</param>
    /// <returns></returns>
    public static bool TryGetContentBetweenMarkers(string text, MarkerSpan? start, MarkerSpan? end, out string? content)
    {
        // Can we find content?
        if (!ValidStartEndMarkers(start, end, text.Length))
        {
            content = null;
            return false;
        }

        // Find content
        content = text.Substring(start!.End, end!.Start - start.End);
        return true;
    }

    /// <summary>
    ///     Replace the content between two markers in a given text.
    /// </summary>
    /// <param name="text">The text in which to do the replacement.</param>
    /// <param name="start">Start marker.</param>
    /// <param name="end">End marker.</param>
    /// <param name="content">Content to replace.</param>
    /// <param name="newText">The new text.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static bool TryReplaceContentBetweenMarkers(string text, MarkerSpan? start, MarkerSpan? end, string? content,
        out string? newText)
    {
        // Can we find content?
        if (!ValidStartEndMarkers(start, end, text.Length))
        {
            newText = null;
            return false;
        }

        // Replace content
        var builder = new StringBuilder();
        builder.Append(text.Substring(0, start!.End));
        if (content != null)
            builder.Append(content);
        builder.Append(text.Substring(end!.Start, text.Length - end.Start));
        newText = builder.ToString();
        return true;
    }

    /// <summary>
    ///     Check if two markers are following each other in a given text (in terms of their location).
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="textLength"></param>
    /// <returns></returns>
    private static bool ValidStartEndMarkers(MarkerSpan? start, MarkerSpan? end, int textLength)
    {
        return !(
            // Markers not provided
            start == null || end == null ||

            // Start out of bound
            start.End > textLength || start.Start < 0 ||

            // End out of bound
            end.End > textLength || end.Start < 0 ||

            // Start after end
            start.Start > end.Start
        );
    }
}