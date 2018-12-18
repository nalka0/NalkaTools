using System;
using System.Collections.Generic;
using System.Reflection;
using UnityObject = UnityEngine.Object;

namespace Nalka.Tools.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines if <paramref name="first"/> inherits from <paramref name="other"/>
        /// </summary>
        /// <param name="first">The first <see cref="Type"/> to compare</param>
        /// <param name="other">The other <see cref="Type"/> to compare</param>
        /// <returns></returns>
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
            //one liner version
            //return other == null || first == null ? false : first.BaseType == other ? true : first.BaseType != null ? first.BaseType.Inherits(other) : false;
        }
    }

    public static class ClassesExtensions
    {
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

    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Returns the number of weeks (7 days periods) in the given <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="timeSpan"><see cref="TimeSpan"/> to get the week count of</param>
        /// <returns>The number of weeks (7 days periods) in the given <see cref="TimeSpan"/></returns>
        public static double GetTotalWeeks(this TimeSpan timeSpan) => timeSpan.TotalDays / 7;

        /// <summary>
        /// Returns the number of Years (365.25 days periods) in the given <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="timeSpan"><see cref="TimeSpan"/> to get the years count of</param>
        /// <returns>Returns the number of Years (365.25 days periods) in the given <see cref="TimeSpan"/></returns>
        public static double GetTotalYears(this TimeSpan timeSpan) => timeSpan.TotalDays / 365.25;
    }

    internal class RefNeeded
    {
        public bool RefCancel { get; set; } = false;
    }

}