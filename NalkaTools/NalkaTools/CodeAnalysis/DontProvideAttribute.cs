using System;
using static System.AttributeTargets;

namespace NalkaTools.CodeAnalysis
{
    /// <summary>
    /// Can be used on parameters declaration to tell they shouldn't be provided.
    /// </summary>
    [AttributeUsage(Parameter, Inherited = true, AllowMultiple = false)]
    public class DontProvideAttribute : Attribute
    {
    }
}
