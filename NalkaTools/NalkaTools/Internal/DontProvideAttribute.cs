using System;
using static System.AttributeTargets;

namespace Nalka.Tools.Internal
{
    /// <summary>
    /// Can be used on parameters declaration to tell they shouldn't be provided.
    /// </summary>
    [AttributeUsage(Parameter, Inherited = true, AllowMultiple = false)]
    internal class DontProvideAttribute : Attribute
    {

    }
}
