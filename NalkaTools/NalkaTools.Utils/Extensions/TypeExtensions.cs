using System;

namespace NalkaTools.Utils.Extensions
{
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
}
