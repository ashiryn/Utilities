using System.Text;

namespace FluffyVoid.Utilities.Formatters;

/// <summary>
///     String formatter designed to iterate over a collection and format each item using a targeted string formatter for the items
/// </summary>
/// <typeparam name="TCollection">The type of collection to format into a string</typeparam>
/// <typeparam name="TType">The type of data the collection holds</typeparam>
public class CollectionFormatter<TCollection, TType>
    : IStringFormatter<TCollection>
    where TCollection : IEnumerable<TType>
{
    /// <summary>
    ///     The type of string formatter to use for each item in the collection
    /// </summary>
    private readonly IStringFormatter<TType> _formatter;

    /// <summary>
    ///     Constructor used to initialize the formatter
    /// </summary>
    /// <param name="formatter">The type of string formatter to use for each item in the collection</param>
    public CollectionFormatter(IStringFormatter<TType> formatter)
    {
        _formatter = formatter;
    }
    /// <summary>
    ///     Formats the passed in object into a user-friendly readable string
    /// </summary>
    /// <param name="value">The object to format</param>
    /// <returns>The user-friendly readable string</returns>
    public string? ToString(TCollection? value)
    {
        if (value == null)
        {
            return "null";
        }

        StringBuilder result = new StringBuilder();
        foreach (TType item in value)
        {
            if (item == null)
            {
                result.AppendLine("null");
                continue;
            }

            result.AppendLine(_formatter.ToString(item));
        }

        return result.ToString().TrimEnd('\n');
    }
    /// <summary>
    ///     Formats the passed in object into a user-friendly readable string
    /// </summary>
    /// <param name="value">The object to format</param>
    /// <returns>The user-friendly readable string</returns>
    public string? ToString(object? value)
    {
        if (value is TCollection list)
        {
            return ToString(list);
        }

        return string.Empty;
    }
}