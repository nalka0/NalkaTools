using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
}