using Microsoft.VisualStudio.TestTools.UnitTesting;
using NalkaTools.CSharp.Extensions;
using System;

namespace NalkaTools.CSharp.Tests.ExtensionsTests
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void Inherits()
        {
            Assert.IsTrue(typeof(string).Inherits(typeof(object)));
            Assert.IsFalse(typeof(object).Inherits(typeof(string)));
            Assert.IsTrue(typeof(TestClassAttribute).Inherits(typeof(Attribute)));
        }
    }
}
