namespace FluffyVoid.Utilities.Formatters;

/// <summary>
///     Defines a contract for a class that can format a specific data type into a string representation
/// </summary>
public interface IStringFormatter
{
    /// <summary>
    ///     Formats the passed in object into a user-friendly readable string
    /// </summary>
    /// <param name="value">The object to format</param>
    /// <returns>The user-friendly readable string</returns>
    string? ToString(object? value);
}
/// <summary>
///     Defines a contract for a class that can format a specific data type into a string representation
/// </summary>
/// <typeparam name="TType">The type of object the string formatter should format</typeparam>
public interface IStringFormatter<in TType> : IStringFormatter
{
    /// <summary>
    ///     Formats the passed in object into a user-friendly readable string
    /// </summary>
    /// <param name="value">The object to format</param>
    /// <returns>The user-friendly readable string</returns>
    string? ToString(TType? value);
}