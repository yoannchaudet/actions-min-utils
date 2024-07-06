using System.Collections.ObjectModel;

namespace ActionsMinUtils.templating;

/// <summary>
///     A marker in a text file.
/// </summary>
public class Marker
{
    /// <summary>
    ///     The arguments.
    /// </summary>
    private readonly Dictionary<string, string> _arguments;

    /// <summary>
    ///     Default ctor.
    /// </summary>
    /// <param name="name">Name of the marker</param>
    /// <param name="arguments">Its arguments</param>
    internal Marker(string name, Dictionary<string, string> arguments)
    {
        Name = name;
        _arguments = arguments;
    }

    /// <summary>
    ///     Default ctor.
    /// </summary>
    /// <param name="name">Name of the marker</param>
    public Marker(string name) : this(name, new Dictionary<string, string>())
    {
    }

    /// <summary>
    ///     Name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Arguments.
    /// </summary>
    public IReadOnlyDictionary<string, string> Arguments => new ReadOnlyDictionary<string, string>(_arguments);

    /// <summary>
    ///     Add an argument.
    /// </summary>
    /// <param name="key">The argument's key</param>
    /// <param name="value">The argument's value</param>
    /// <returns></returns>
    public Marker AddArgument(string key, string value)
    {
        _arguments[key] = value;
        return this;
    }
}