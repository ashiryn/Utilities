using FluffyVoid.Utilities.Formatters;

namespace FluffyVoid.Utilities;

/// <summary>
///     Organizational class for utilities that relate to arrays
/// </summary>
public static class ArrayUtility
{
    /// <summary>
    ///     Determines weather a specified index is within the bounds of an array
    /// </summary>
    /// <param name="source">Reference to the array calling the function</param>
    /// <param name="index">The desired index to check</param>
    /// <typeparam name="T">The type of the array's elements</typeparam>
    /// <returns>True if the index is within the bounds of the array, otherwise false</returns>
    public static bool InBounds<T>(this T[] source, int index)
    {
        return index < source.Length && index >= 0;
    }
    /// <summary>
    ///     Converts an array to a string representation using the passed in string formatter
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