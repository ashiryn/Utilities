namespace FluffyVoid.Utilities;

/// <summary>
///     Organizational class for utilities that relate to spans
/// </summary>
public static class SpanUtility
{
    /// <summary>
    ///     Splits a readonly span and returns the two halves of the string
    /// </summary>
    /// <param name="value">The span to perform the split on</param>
    /// <param name="delimiter">The character to use as the delimiter</param>
    /// <param name="remainder">The right side of the string after the split</param>
    /// <returns>The left side of the string after the split</returns>
    public static ReadOnlySpan<char> Split(this ReadOnlySpan<char> value,
                                           char delimiter,
                                           out ReadOnlySpan<char> remainder)
    {
        ReadOnlySpan<char> result = value;
        if (value.IsEmpty)
        {
            result = null;
        }

        remainder = ReadOnlySpan<char>.Empty;
        int delimiterIndex = value.IndexOf(delimiter);
        if (delimiterIndex > 0)
        {
            result = value.Slice(0, delimiterIndex);
            remainder = value.Slice(delimiterIndex + 1);
        }

        return result;
    }
    /// <summary>
    ///     Splits a readonly span and returns the two halves of the string
    /// </summary>
    /// <param name="value">The span to perform the split on</param>
    /// <param name="delimiters">The array of characters to use as a delimiter</param>
    /// <param name="remainder">The right side of the string after the split</param>
    /// <returns>The left side of the string after the split</returns>
    public static ReadOnlySpan<char> Split(this ReadOnlySpan<char> value,
                                           char[] delimiters,
                                           out ReadOnlySpan<char> remainder)
    {
        ReadOnlySpan<char> result = value;
        if (value.IsEmpty)
        {
            result = null;
        }

        remainder = ReadOnlySpan<char>.Empty;
        int delimiterIndex = value.IndexOfAny(delimiters);
        if (delimiterIndex > 0)
        {
            result = value.Slice(0, delimiterIndex);
            remainder = value.Slice(delimiterIndex + 1);
        }

        return result;
    }
}