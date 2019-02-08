using System;
using System.Collections.Generic;
using System.Reflection;
using UnityObject = UnityEngine.Object;

namespace Nalka.Tools.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="int"/>
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Determines if <paramref name="current"/> is between <paramref name="minValue"/> and <paramref name="maxValue"/> 
        /// </summary>
        /// <param name="current">The <see cref="int"/> to compare with <paramref name="minValue"/> and <paramref name="maxValue"/></param>
        /// <param name="minValue">The minimum value for <paramref name="current"/> in order to return <see langword="true"/>. This parameter is EXLCUSIVE</param>
        /// <param name="maxValue">The maximum value for <paramref name="current"/> in order to return <see langword="true"/>. This parameter is EXLCUSIVE</param>
        /// <returns><see langword="true"/> if <paramref name="current"/> is strictly higher than <paramref name="minValue"/> and stricly lower than <paramref name="maxValue"/>. <see langword="false"/> otherwise</returns>
        public static bool IsBetween(this int current, int minValue, int maxValue)
        {
            8.IsBetween(7, 9);
            return current > minValue && current < maxValue;
        }
    }
    /// <summary>
    /// Provides extension methods for <see cref="Type"/>
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines if <paramref name="first"/> inherits from <paramref name="other"/>
        /// </summary>
        /// <param name="first">The first <see cref="Type"/> to compare</param>
        /// <param name="other">The other <see cref="Type"/> to compare</param>
        /// <returns><see langword="true"/> if <paramref name="first"/> inherits from <paramref name="other"/>, <see langword="false"/> otherwise</returns>
        public static bool Inherits(this Type first, Type other)
        {
            if (other == null || first == null)
            {
                return false;
            }
            if (first.BaseType == other)
            {
                return true;
            }
            return first.BaseType != null ? first.BaseType.Inherits(other) : false;
        }
    }
    /// <summary>
    /// Provides extensions for classes
    /// </summary>
    [Obsolete("Not ready", true)]
    public static class ClassesExtensions
    {
        /// <summary>
        /// Determines by comparing every member if <paramref name="first"/> is equal to <paramref name="other"/>
        /// </summary>
        /// <typeparam name="T">Type of the classes to compare</typeparam>
        /// <param name="first">First <typeparamref name="T"/> to compare</param>
        /// <param name="other">Other <typeparamref name="T"/> to compare</param>
        /// <returns><see langword="true"/> if every member from <paramref name="first"/> equals the correspoding member from <paramref name="other"/>, <see langword="false"/> otherwise</returns>
        [Obsolete("Not ready", true)]
        public static bool MemberwiseEquals<T>(this T first, T other) where T : class
        {
            foreach (MemberInfo member in first.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (member is ConstructorInfo || member is MethodInfo)
                    continue;
            }
            return true;
        }
    }
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

    internal class RefNeeded
    {
        public bool RefCancel { get; set; } = false;
    }

}