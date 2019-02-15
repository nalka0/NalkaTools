using System.IO;

namespace NalkaTools.CSharp.Files
{
    /// <summary>
    /// Provides tools for finding an available file path
    /// </summary>
    public static class PathBuilder
    {
        /// <summary>
        /// Finds the first available filename by adding numbers after <paramref name="initialPath"/>
        /// </summary>
        /// <param name="initialPath">The initial path to be checked without the file extension</param>
        /// <param name="extension">The file's extension (<see cref="string.Empty"/> or <see langword="null"/> if no extension should be added)</param>
        /// <param name="maxTries">The number of maximum attempts before throwing an exception</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"> when no available path is found after <paramref name="maxTries"/> attemtps</exception>
        public static string GetAvailableFileName(string initialPath, string extension, uint maxTries = uint.MaxValue)
        {
            if (!extension.StartsWith(".") && !string.IsNullOrEmpty(extension))
            {
                extension = extension.Insert(0, ".");
            }

            uint counter = 1;
            while (counter <= maxTries - 1 && File.Exists($"{initialPath}{counter}{extension}"))
            {
                counter++;
            }

            if (counter > maxTries)
            {
                throw new FileNotFoundException($"Couldn't find an available file name for {initialPath}");
            }

            return $"{initialPath}{counter}{extension}";
        }
    }
}