using System.Globalization;

namespace FluffyVoid.Utilities
{
    /// <summary>
    ///     Organizational class for utilities that relate to DateTimes
    /// </summary>
    public static class DateTimeUtility
    {
        /// <summary>
        ///     Converts a DateTime object to an RFC3339 formatted string
        /// </summary>
        /// <param name="dateTime">The DateTime to convert</param>
        /// <returns>Time in RFC3339 string format</returns>
        public static string ToRfc3339String(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz",
                                     DateTimeFormatInfo.InvariantInfo);
        }
    }
}
