using System.ComponentModel;
using System.Reflection;
using FluffyVoid.Utilities.Formatters;

namespace FluffyVoid.Utilities;

/// <summary>
///     Organizational class for utilities that relate to Enums
/// </summary>
public static partial class EnumUtility
{
    /// <summary>
    ///     Parses the description attribute from an Enum to allow string formatting of an Enum to be separated from coding
    ///     standards
    /// </summary>
    /// <param name="value">The enum to get the description from</param>
    /// <typeparam name="TEnum">The type of enum to search</typeparam>
    /// <returns>The description string from the attribute if it exists, otherwise the ToString() of the enum</returns>
    public static string GetDescription<TEnum>(this TEnum value)
        where TEnum : Enum
    {
        FieldInfo? field = value.GetType().GetField(value.ToString());
        DescriptionAttribute?[]? attributes = field
                                              ?.GetCustomAttributes<
                                                  DescriptionAttribute>(false)
                                              .ToArray();

        return attributes != null && attributes.Length != 0 &&
               attributes[0] != null
            ? attributes[0]!.Description
            : value.ToString();
    }
    /// <summary>
    ///     Retrieves the particular description attribute from an Enum
    /// </summary>
    /// <param name="value">The enum to get the description from</param>
    /// <typeparam name="TEnum">The type of enum to search</typeparam>
    /// <typeparam name="TDescription">The type of description attribute to use</typeparam>
    /// <returns>The description attribute found if it exists, otherwise null</returns>
    public static TDescription? GetDescription<TEnum, TDescription>(
        this TEnum value)
        where TEnum : Enum
        where TDescription : DescriptionAttribute
    {
        FieldInfo? field = value.GetType().GetField(value.ToString());
        TDescription?[]? attributes =
            field?.GetCustomAttributes<TDescription>(false).ToArray();

        return attributes != null && attributes.Length != 0 &&
               attributes[0] != null
            ? attributes[0]
            : null;
    }

    /// <summary>
    ///     Increments the enum to the next enum value
    /// </summary>
    /// <param name="source">The enum value to increment</param>
    /// <typeparam name="T">The enum type to use</typeparam>
    /// <returns>The next enum entry in the list, or the 1st entry if incrementing past the end</returns>
    public static T Next<T>(this T source)
        where T : Enum
    {
        T[] entries = (T[])Enum.GetValues(source.GetType());
        int entryIndex = Array.IndexOf(entries, source) + 1;
        return entries.Length == entryIndex ? entries[0] : entries[entryIndex];
    }
    /// <summary>
    ///     Decrements the enum to the previous enum value
    /// </summary>
    /// <param name="source">The enum value to decrement</param>
    /// <typeparam name="T">The enum type to use</typeparam>
    /// <returns>The previous enum entry in the list, or the last entry if decrementing past the 1st entry</returns>
    public static T Previous<T>(this T source)
        where T : Enum
    {
        T[] entries = (T[])Enum.GetValues(source.GetType());
        int entryIndex = Array.IndexOf(entries, source) - 1;
        return entryIndex < 0 ? entries[^1] : entries[entryIndex];
    }
    /// <summary>
    ///     Converts an enum to its integer value
    /// </summary>
    /// <param name="value">The enum to convert</param>
    /// <returns>The converted integer value</returns>
    public static int ToInt(this Enum value)
    {
        return Convert.ToInt32(value);
    }
    /// <summary>
    ///     Converts an enum to its short value
    /// </summary>
    /// <param name="value">The enum to convert</param>
    /// <returns>The converted integer value</returns>
    public static short ToShort(this Enum value)
    {
        return Convert.ToInt16(value);
    }
    /// <summary>
    ///     Converts an enum to a string representation using the passed in string formatter
    /// </summary>
    /// <param name="value">The enum to convert</param>
    /// <param name="formatter">The string formatter to use when converting to a string</param>
    /// <returns>The user-friendly readable string</returns>
    public static string? ToString(this Enum value, IStringFormatter? formatter)
    {
        return formatter != null ? formatter.ToString(value) : value.ToString();
    }
    /// <summary>
    ///     Converts an enum to its unsigned integer value
    /// </summary>
    /// <param name="value">The enum to convert</param>
    /// <returns>The converted integer value</returns>
    public static uint ToUint(this Enum value)
    {
        return Convert.ToUInt32(value);
    }
    /// <summary>
    ///     Converts an enum to its unsigned short value
    /// </summary>
    /// <param name="value">The enum to convert</param>
    /// <returns>The converted integer value</returns>
    public static ushort ToUshort(this Enum value)
    {
        return Convert.ToUInt16(value);
    }
}