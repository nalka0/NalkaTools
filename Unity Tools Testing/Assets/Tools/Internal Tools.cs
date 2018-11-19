using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Nalka.Tools.Extensions
{
    public static class TypeExtensions
    {
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
        
    public static class GenericExtensions
    {
        public static bool MemberwiseEquals<T>(this T first, T other) where T : class
        {
            foreach (MemberInfo member in first.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (member is ConstructorInfo || member is MethodInfo)
                    continue;
                Debug.Log($"{member.Name}");
            }
            return true;
        }
    }
}