namespace System
{
    /// <summary>
    /// Various extension methods for the DateTime class
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets the first day of the week from the date
        /// </summary>
        /// <param name="date">The date</param>
        /// <param name="firstDay">The first day of the week</param>
        /// <returns>A date representing the start of the week</returns>
        public static DateTime StartOfWeek
            (
                this DateTime date,
                DayOfWeek firstDay
            )
        {
            int diff = (7 + (date.DayOfWeek - firstDay)) % 7;

            return date.AddDays(-1 * diff).Date;
        }

        /// <summary>
        /// Gets the last day of the week from the date
        /// </summary>
        /// <param name="date">The date</param>
        /// <param name="firstDay">The first day of the week</param>
        /// <returns>A date representing the end of the week</returns>
        public static DateTime EndOfWeek
            (
                this DateTime date,
                DayOfWeek firstDay
            )
        {
            var firstDayDate = date.StartOfWeek(firstDay);

            return firstDayDate.AddDays(7);
        }
    }
}
