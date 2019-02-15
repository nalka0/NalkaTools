namespace NalkaTools.CSharp.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="int"/>
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Determines if <paramref name="current"/> is between <paramref name="firstValue"/> and <paramref name="secondValue"/> 
        /// </summary>
        /// <param name="current">The <see cref="int"/> to compare with <paramref name="firstValue"/> and <paramref name="secondValue"/></param>
        /// <param name="firstValue">The first value to compare with <paramref name="current"/>. This parameter is EXLCUSIVE</param>
        /// <param name="secondValue">The second value to compare with <paramref name="current"/>. This parameter is EXLCUSIVE</param>
        /// <returns><see langword="true"/> if <paramref name="current"/> is between <paramref name="firstValue"/> and <paramref name="secondValue"/>. <see langword="false"/> otherwise</returns>
        public static bool IsBetween(this int current, int firstValue, int secondValue)
        {
            if (firstValue == secondValue)
            {
                return false;
            }
            else if (firstValue < secondValue)
            {
                return current > firstValue && current < secondValue;
            }
            else
            {
                return current < firstValue && current > secondValue;
            }
        }
    }
}