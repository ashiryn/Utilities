using System.ComponentModel;
using System.Reflection;

namespace FluffyVoid.Utilities;

public static partial class EnumUtility
{
    /// <summary>
    ///     Parses an Enum to find a match by comparing a passed in string to either the Description Attribute, or name of each
    ///     Enum entry
    /// </summary>
    /// <param name="description">The description to find</param>
    /// <typeparam name="T">The type of enum to search</typeparam>
    /// <returns>
    ///     The found enum if it has a matching description, or if the name of the field matches the description,
    ///     otherwise the last entry is returned
    /// </returns>
    public static T GetValueFromDescription<T>(string description)
        where T : Enum
    {
        Type type = typeof(T);
        foreach (FieldInfo field in type.GetFields())
        {
            if (Attribute.GetCustomAttribute(field,
                                             typeof(DescriptionAttribute)) is
                DescriptionAttribute attribute)
            {
                if (attribute.Description == description ||
                    field.Name == description)
                {
                    return (T)field.GetValue(null)!;
                }
            }

            if (field.Name != description)
            {
                continue;
            }

            return (T)field.GetValue(null)!;
        }

        return Enum.GetValues(type).Cast<T>().Last();
    }
    /// <summary>
    ///     Parses the description attribute from an Enum to allow string formatting of an Enum to be separated from coding
    ///     standards
    /// </summary>
    /// <param name="value">The enum to get the description from</param>
    /// <param name="description">
    ///     The found description string if a description attribute was found, otherwise the
    ///     Enum.ToString()
    /// </param>
    /// <typeparam name="T">The type of enum to search</typeparam>
    /// <returns>True if the enum had a description attribute, otherwise false</returns>
    public static bool TryGetDescription<T>(T value, out string description)
        where T : Enum
    {
        FieldInfo? field = value.GetType().GetField(value.ToString());
        DescriptionAttribute?[]? attributes = field
                                              ?.GetCustomAttributes<
                                                  DescriptionAttribute>(false)
                                              .ToArray();

        if (attributes != null && attributes.Length != 0 &&
            attributes[0] != null)
        {
            description = attributes[0]!.Description;
            return true;
        }

        description = value.ToString();
        return false;
    }
    /// <summary>
    ///     Tries to parse an Enum to find a match by comparing a passed in string to either the Description Attribute, or name
    ///     of each Enum entry
    /// </summary>
    /// <param name="description">The description to find</param>
    /// <param name="value">The matching enum value if a match was found, otherwise the last entry</param>
    /// <typeparam name="T">The type of enum to search</typeparam>
    /// <returns>True if a match was found, otherwise false</returns>
    public static bool TryGetValueFromDescription<T>(
        string description, out T value)
        where T : Enum
    {
        Type type = typeof(T);
        foreach (FieldInfo field in type.GetFields())
        {
            if (Attribute.GetCustomAttribute(field,
                                             typeof(DescriptionAttribute)) is
                DescriptionAttribute attribute)
            {
                if (attribute.Description == description ||
                    field.Name == description)
                {
                    value = (T)field.GetValue(null)!;
                    return true;
                }
            }

            if (field.Name != description)
            {
                continue;
            }

            value = (T)field.GetValue(null)!;
            return true;
        }

        value = Enum.GetValues(type).Cast<T>().Last();
        return false;
    }
}