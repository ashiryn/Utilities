using FluffyVoid.Utilities.Formatters;

namespace FluffyVoid.Utilities;

/// <summary>
///     Organizational class for utilities that relate to lists
/// </summary>
public static class ListUtility
{
    /// <summary>
    ///     Determines weather a specified index is within the bounds of an list
    /// </summary>
    /// <param name="source">Reference to the list calling the function</param>
    /// <param name="index">The desired index to check</param>
    /// <typeparam name="T">The type of the list's elements</typeparam>
    /// <returns>True if the index is within the bounds of the list, otherwise false</returns>
    public static bool InBounds<T>(this List<T> source, int index)
    {
        return index < source.Count && index >= 0;
    }
    /// <summary>
    ///     Converts a list to a string representation using the passed in string formatter
    /// </summary>
    /// <param name="value">The enum to convert</param>
    /// <param name="formatter">The string formatter to use when converting to a string</param>
    /// <returns>The user-friendly readable string</returns>
    public static string? ToString<T>(this T[] value,
                                      IStringFormatter? formatter)
    {
        return formatter != null ? formatter.ToString(value) : value.ToString();
    }
}