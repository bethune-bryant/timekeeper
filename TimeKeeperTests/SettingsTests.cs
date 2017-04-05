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
    public class SettingsTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SettingsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SettingsTest1()
        {
            Assert.Fail();
        }
        
        [TestMethod()]
        public void LastUnclosedTaskTest()
        {
            Settings test = new Settings();

            Assert.AreEqual(null, test.LastUnclosedTask);

            test.TimeEntries.Add(new TimeEntry("Test Project", "Test Task1", "Test Employer", DateTime.Now));

            TimeEntry temp = test.LastUnclosedTask;

            Assert.AreEqual("Test Project", temp.Project);
            Assert.AreEqual("Test Task1", temp.Task);

            test.TimeEntries.Remove(temp);

            Assert.AreEqual(null, test.LastUnclosedTask);

            temp.Stop = DateTime.Now;

            test.TimeEntries.Add(temp);

            Assert.AreEqual(null, test.LastUnclosedTask);
        }
    }
}