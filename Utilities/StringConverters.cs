namespace FluffyVoid.Utilities;

public static partial class StringUtility
{
    /// <summary>
    ///     Converts a string into an array of bytes
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>An array of bytes if the conversion was successful, otherwise default(byte[])</returns>
    public static byte[]? ToByteArray(this string[] value)
    {
        if (value.Length <= 0)
        {
            return null;
        }

        byte[] result = new byte[value.Length];
        for (int i = 0; i < value.Length; ++i)
        {
            result[i] = value[i].ToByte();
        }

        return result;
    }
    /// <summary>
    ///     Converts a string into an array of chars
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>An array of chars if the conversion was successful, otherwise default(char[])</returns>
    public static char[]? ToCharArray(this string[] value)
    {
        if (value.Length <= 0)
        {
            return null;
        }

        char[] result = new char[value.Length];
        for (int i = 0; i < value.Length; ++i)
        {
            result[i] = value[i].ToChar();
        }

        return result;
    }
    /// <summary>
    ///     Converts a string into an array of SBytes
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>An array of SBytes if the conversion was successful, otherwise default(sbyte[])</returns>
    public static sbyte[]? ToSbyteArray(this string[] value)
    {
        if (value.Length <= 0)
        {
            return null;
        }

        sbyte[] result = new sbyte[value.Length];
        for (int i = 0; i < value.Length; ++i)
        {
            result[i] = value[i].ToSbyte();
        }

        return result;
    }
}