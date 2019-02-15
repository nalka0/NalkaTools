using Microsoft.VisualStudio.TestTools.UnitTesting;
using NalkaTools.CSharp.Extensions;
using System;

namespace NalkaTools.CSharp.Tests.ExtensionsTests
{
    [TestClass]
    public class TimeSpanExtensionsTests
    {
        [TestMethod]
        public void GetTotalWeeks()
        {
            Assert.IsTrue(TimeSpan.FromDays(14).GetTotalWeeks().Equals(2));
            Assert.IsTrue(TimeSpan.FromDays(7).GetTotalWeeks().Equals(1));
            Assert.IsTrue(TimeSpan.FromDays(10).GetTotalWeeks().Equals((double)10 / 7));
            Assert.IsTrue(TimeSpan.FromDays(0).GetTotalWeeks().Equals(0));
        }

        [TestMethod]
        public void GetTotalYears()
        {
            Assert.IsTrue(TimeSpan.FromDays(1461).GetTotalYears().Equals(4));
            Assert.IsTrue(TimeSpan.FromDays(730.5).GetTotalYears().Equals(2));
            Assert.IsTrue(TimeSpan.FromDays(246).GetTotalYears().Equals((double)246 / 365.25));
            Assert.IsTrue(TimeSpan.FromDays(0).GetTotalYears().Equals(0));
        }
    }
}
