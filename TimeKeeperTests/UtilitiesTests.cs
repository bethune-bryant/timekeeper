using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.Tests
{
    [TestClass()]
    public class UtilitiesTests
    {
        [TestMethod()]
        public void GetWeekTest()
        {
            Assert.AreEqual(53, Utilities.GetWeek(new DateTime(1999, 1, 1)), "Year/Week wraparound.");
            Assert.AreEqual(53, Utilities.GetWeek(new DateTime(1999, 1, 2)), "Year/Week wraparound.");
            Assert.AreEqual(53, Utilities.GetWeek(new DateTime(1999, 1, 3)), "Year/Week wraparound.");
            Assert.AreEqual(1, Utilities.GetWeek(new DateTime(1999, 1, 4)), "Year/Week wraparound.");
            Assert.AreEqual(1, Utilities.GetWeek(new DateTime(1999, 1, 5)), "Year/Week wraparound.");
            Assert.AreEqual(1, Utilities.GetWeek(new DateTime(1999, 1, 6)), "Year/Week wraparound.");
        }

        [TestMethod()]
        public void ElapsedTimeStringTest()
        {
            DateTime now = DateTime.Now;

            Assert.AreEqual("00:00", Utilities.ElapsedTimeString(now, now), "Zero time.");
            Assert.AreEqual("02:00", Utilities.ElapsedTimeString(now, now.AddHours(2)), "Zero minutes.");
            Assert.AreEqual("02:00", Utilities.ElapsedTimeString(now, now.AddHours(-2)), "Negative time.");
            Assert.AreEqual("10:00", Utilities.ElapsedTimeString(now, now.AddHours(10)), "Double digit hours.");
            Assert.AreEqual("15:00", Utilities.ElapsedTimeString(now, now.AddHours(15)), "Over 12 hours.");
            Assert.AreEqual("02:01", Utilities.ElapsedTimeString(now, now.AddHours(2).AddMinutes(1)), "One minute.");
            Assert.AreEqual("02:59", Utilities.ElapsedTimeString(now, now.AddHours(2).AddMinutes(59)), "59 minutes.");
        }

        [TestMethod()]
        public void GetTimeTest()
        {
            DateTime now = DateTime.Now;
            List<TimeEntry> entries = new List<TimeEntry>();

            Assert.AreEqual(new TimeSpan(0,0,0), Utilities.GetTime(entries), "Empty list of entries.");

            entries.Add(new TimeEntry("", "", "", now.AddHours(-4), now.AddHours(-2), ""));

            Assert.AreEqual(new TimeSpan(2, 0, 0), Utilities.GetTime(entries), "One entry.");

            entries.Add(new TimeEntry("", "", "", now.AddHours(-2)));

            Assert.AreEqual(4, Utilities.GetTime(entries).TotalHours, 1.0 / 60.0, "Unclosed entry.");
        }
    }
}