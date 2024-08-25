using NUnit.Framework;
using System;

namespace StudentApp.Tests
{
    [TestFixture()]
    public class DateTimeExtensions_Test
    {
        [Test()]
        public void DurationInYears_Test()
        {
            var duration = DateTime.Parse("2000/01/01").DurationInYears(DateTime.Parse("2018/01/01"));
            Assert.IsTrue(duration == 18);
        }

        [Test()]
        public void ToUnixTimeSeconds_Test()
        {
            var seconds = DateTime.Parse("2000/01/01").ToUnixTimeSeconds();
            Assert.IsTrue(seconds == 946677600);
        }

        [Test()]
        public void UnixTimeSecondsToDateTime_Test()
        {
            var date = ((long)946677600).UnixTimeSecondsToDateTime();
            Assert.IsTrue(date == DateTime.Parse("2000/01/01"));
        }
    }
}
