using Microsoft.VisualStudio.TestTools.UnitTesting;
using NalkaTools.CSharp.Extensions;

namespace NalkaTools.CSharp.Tests.ExtensionsTests
{
    [TestClass]
    public class IntExtensionsTests
    {
        [TestMethod]
        public void IsBetween()
        {
            Assert.IsTrue(9.IsBetween(10, 8));
            Assert.IsTrue((-65).IsBetween(-100, 47));
            Assert.IsFalse(6600.IsBetween(6600, 6600));
            Assert.IsFalse(6600.IsBetween(6689, 9600));
            Assert.IsFalse(6600.IsBetween(5426, 3258));
        }
    }
}
