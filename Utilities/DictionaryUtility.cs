using FluffyVoid.Utilities.Formatters;

namespace FluffyVoid.Utilities;

/// <summary>
///     Organizational class for utilities that relate to dictionaries
/// </summary>
public static class DictionaryUtility
{
    /// <summary>
    ///     Converts a dictionary to a string representation using the passed in string formatter
    /// </summary>
    /// <param name="value">The enum to convert</param>
    /// <param name="formatter">The string formatter to use when converting to a string</param>
    /// <returns>The user-friendly readable string</returns>
    public static string? ToString<T>(this T[] value,
                                      IStringFormatter? formatter)
    {
        return formatter != null ? formatter.ToString(value) : value.ToString();
    }
    /// <summary>
    ///     Gets the value associated with the specified key and casts to the desired type if able.
    /// </summary>
    /// <param name="source">Reference to the source dictionary to get the converted value out of</param>
    /// <param name="key">The key to retrieve a value from</param>
    /// <param name="result">The converted result if it was in the dictionary and of the type requested</param>
    /// <typeparam name="TKey">The type used for the key</typeparam>
    /// <typeparam name="TValue">The type used for the value</typeparam>
    /// <typeparam name="TConverted">The type to convert the value to if able</typeparam>
    /// <returns>True if the key has a value and is of the requested type, otherwise false</returns>
    public static bool TryGetValue<TKey, TValue, TConverted>(
        this Dictionary<TKey, TValue> source, TKey key, out TConverted? result)
        where TKey : notnull
    {
        if (source.TryGetValue(key, out TValue? value) &&
            value is TConverted converted)
        {
            result = converted;
            return true;
        }

        result = default;
        return false;
    }
}