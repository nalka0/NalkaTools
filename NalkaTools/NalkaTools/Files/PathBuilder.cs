﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Nalka.Tools.Files
{
    /// <summary>
    /// Provides tools for finding good path
    /// </summary>
    public static class PathBuilder
    {
        /// <summary>
        /// Finds the first filename available by adding numbers after <paramref name="InitialPath"/>
        /// </summary>
        /// <param name="InitialPath">The initial path to be checked without the file extension</param>
        /// <param name="Extension">The file's extension (<see cref="string.Empty"/> or <see langword="null"/> if no extension should be added)</param>
        /// <param name="MaxTries"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"> when no available path is found</exception>
        public static string GetAvailableFileName(string InitialPath, string Extension, uint MaxTries = uint.MaxValue)
        {
            if (!Extension.StartsWith(".") && !string.IsNullOrEmpty(Extension))
                Extension = Extension.Insert(0, ".");
            uint counter = 1;
            while (counter <= MaxTries - 1 && File.Exists($"{InitialPath}{counter}{Extension}"))
                counter++;
            if (counter > MaxTries)
                throw new FileNotFoundException($"Couldn't find an available file name for {InitialPath}");
            return $"{InitialPath}{counter}{Extension}";
        }
    }
}