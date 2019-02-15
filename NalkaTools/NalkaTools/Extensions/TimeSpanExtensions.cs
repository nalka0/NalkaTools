namespace NalkaTools.CSharp.Extensions
{
    using System;

    /// <summary>
    /// Provides extensions method for <see cref="TimeSpan"/>
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Returns the number of weeks (7 days periods) in the given <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="timeSpan"><see cref="TimeSpan"/> to get the week count of</param>
        /// <returns>The number of weeks (7 days periods) in the given <see cref="TimeSpan"/></returns>
        public static double GetTotalWeeks(this TimeSpan timeSpan)
        {
            return timeSpan.TotalDays / 7;
        }

        /// <summary>
        /// Returns the number of years (365.25 days periods) in the given <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="timeSpan"><see cref="TimeSpan"/> to get the years count of</param>
        /// <returns>The number of years (365.25 days periods) in the given <see cref="TimeSpan"/></returns>
        public static double GetTotalYears(this TimeSpan timeSpan)
        {
            return timeSpan.TotalDays / 365.25;
        }
    }
}
