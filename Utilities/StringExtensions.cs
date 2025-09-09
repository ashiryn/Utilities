using System.Net;
using System.Text;

namespace FluffyVoid.Utilities;

/// <summary>
///     Organizational class for utilities that relate to strings
/// </summary>
public static partial class StringUtility
{
    /// <summary>
    ///     List of characters that are invalid for using within file names
    /// </summary>
    private static readonly char[] InvalidFileNameChars =
        Path.GetInvalidFileNameChars();
    /// <summary>
    ///     Converts a string into a bool
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>The bool if conversion was successful, otherwise default(bool)</returns>
    public static bool ToBool(this string value)
    {
        return bool.TryParse(value, out bool result) && result;
    }

    /// <summary>
    ///     Converts a string into a byte
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>The byte if conversion was successful, otherwise default(byte)</returns>
    public static byte ToByte(this string value)
    {
        return byte.TryParse(value, out byte result) ? result : (byte)0;
    }
    /// <summary>
    ///     Converts a string to a char
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>The char if conversion was successful, otherwise default(char)</returns>
    public static char ToChar(this string value)
    {
        return char.TryParse(value, out char result) ? result : '\0';
    }
    /// <summary>
    ///     Converts a string to a decimal
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>The decimal if conversion was successful, otherwise default(decimal)</returns>
    public static decimal ToDecimal(this string value)
    {
        return decimal.TryParse(value, out decimal result) ? result : 0;
    }
    /// <summary>
    ///     Converts a string to a double
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>The double if conversion was successful, otherwise default(double)</returns>
    public static double ToDouble(this string value)
    {
        return double.TryParse(value, out double result) ? result : 0;
    }
    /// <summary>
    ///     Converts a string to a float
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>The float if conversion was successful, otherwise default(float)</returns>
    public static float ToFloat(this string value)
    {
        return float.TryParse(value, out float result) ? result : 0;
    }
    /// <summary>
    ///     Converts a string to an int
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>The int if conversion was successful, otherwise default(int)</returns>
    public static int ToInt(this string value)
    {
        return int.TryParse(value, out int result) ? result : 0;
    }
    /// <summary>
    ///     Converts a string to an IPEndPoint
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>The IPEndPoint if conversion was successful, otherwise null</returns>
    public static IPEndPoint? ToIPEndPoint(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        string[] components = value.Split(':');
        return new IPEndPoint(IPAddress.Parse(components[0]),
                              Convert.ToInt32(components[1]));
    }
    /// <summary>
    ///     Converts a string to a long
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>The long if conversion was successful, otherwise default(long)</returns>
    public static long ToLong(this string value)
    {
        return long.TryParse(value, out long result) ? result : 0;
    }
    /// <summary>
    ///     Replaces any invalid file name characters with the '-' character to ensure safe file paths
    /// </summary>
    /// <param name="value">The string to check for valid characters</param>
    /// <returns>The safe file name for use within the File System</returns>
    public static string ToSafeFileName(this string value)
    {
        return new string(value.Select(character =>
                                           InvalidFileNameChars
                                               .Contains(character)
                                               ? '-'
                                               : character).ToArray());
    }
    /// <summary>
    ///     Converts a string into an sbyte
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>The sbyte if conversion was successful, otherwise default(sbyte)</returns>
    public static sbyte ToSbyte(this string value)
    {
        return sbyte.TryParse(value, out sbyte result) ? result : (sbyte)0;
    }
    /// <summary>
    ///     Converts a string to a short
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>The short if conversion was successful, otherwise default(short)</returns>
    public static short ToShort(this string value)
    {
        return short.TryParse(value, out short result) ? result : (short)0;
    }
    /// <summary>
    ///     Converts a string to an uint
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>The uint if conversion was successful, otherwise default(uint)</returns>
    public static uint ToUint(this string value)
    {
        return uint.TryParse(value, out uint result) ? result : 0;
    }
    /// <summary>
    ///     Converts a string to an ulong
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>The ulong if conversion was successful, otherwise default(ulong)</returns>
    public static ulong ToUlong(this string value)
    {
        return ulong.TryParse(value, out ulong result) ? result : 0;
    }
    /// <summary>
    ///     Converts a string to an ushort
    /// </summary>
    /// <param name="value">The string to convert</param>
    /// <returns>The ushort if conversion was successful, otherwise default(ushort)</returns>
    public static ushort ToUshort(this string value)
    {
        return ushort.TryParse(value, out ushort result) ? result : (ushort)0;
    }
    /// <summary>
    ///     Wraps a string by full words with a maximum char count per array element
    /// </summary>
    /// <param name="value">The string to word wrap</param>
    /// <param name="charCount">The maximum number of characters allowed before splitting the string</param>
    /// <returns>An array of string segments that emulate word wrapping</returns>
    public static string[] WordWrap(this string value, int charCount)
    {
        string[] words = value.Split(' ');
        List<string> parts = new List<string>();
        StringBuilder part = new StringBuilder();
        foreach (string currentWord in words)
        {
            if (part.Length + words.Length < charCount)
            {
                part.Append(part.Length <= 0 ? currentWord : $" {currentWord}");
            }
            else
            {
                if (part.Length > 0)
                {
                    parts.Add(part.ToString());
                    part.Clear();
                    part.Append(currentWord);
                }
            }
        }

        parts.Add(part.ToString());
        return parts.ToArray();
    }
}