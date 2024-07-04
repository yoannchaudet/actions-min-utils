namespace ActionsMinUtils.templating;

/// <summary>
///     A marker "span". I.e. a marker parsed in a file.
/// </summary>
public class MarkerSpan(string name, Dictionary<string, string> arguments, int start, int length, int line)
    : Marker(name,
        arguments)
{
    /// <summary>
    ///     The character  index in the file (0-indexed).
    /// </summary>
    public int Start { get; } = start;

    /// <summary>
    ///     The length of the marker in the file.
    /// </summary>
    public int Length { get; } = length;

    /// <summary>
    ///     The line in the file (1-indexed).
    /// </summary>
    public int Line { get; } = line;

    /// <summary>
    ///     Start + length.
    /// </summary>
    public int End => Start + Length;
}