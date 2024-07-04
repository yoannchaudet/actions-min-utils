using Scriban.Runtime;

namespace ActionsMinUtils.templating;

/// <summary>
///     A script object for outputing markers.
/// </summary>
public class MarkersScriptObject : ScriptObject
{
    /// <summary>
    ///     Create an ew marker.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Marker New(string name)
    {
        return new Marker(name);
    }

    /// <summary>
    ///     Add an argument to a marker.
    /// </summary>
    /// <param name="marker"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Marker AddArgument(Marker marker, string key, string value)
    {
        return marker.AddArgument(key, value);
    }

    /// <summary>
    ///     Join multiple values into a single argument.
    /// </summary>
    /// <param name="marker"></param>
    /// <param name="key"></param>
    /// <param name="values"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static Marker JoinArgument(Marker marker, string key, List<string> values,
        char separator = ',')
    {
        if (values.Any())
            return AddArgument(marker, key, string.Join(separator, values));
        return marker;
    }

    /// <summary>
    ///     Output a marker as a string.
    /// </summary>
    /// <param name="marker"></param>
    /// <returns></returns>
    public static string ToString(Marker marker)
    {
        return new MarkersParser().ToText(marker);
    }

    /// <summary>
    ///     Output a custom marker as a string.
    /// </summary>
    /// <param name="marker"></param>
    /// <param name="prefix"></param>
    /// <param name="suffix"></param>
    /// <returns></returns>
    public static string ToCustomString(Marker marker, string prefix, string suffix)
    {
        return new MarkersParser(prefix, suffix).ToText(marker);
    }
}