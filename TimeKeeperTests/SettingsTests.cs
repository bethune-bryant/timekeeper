using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TimeKeeper.Tests
{
    [TestClass()]
    public class SettingsTests
    {
        [TestMethod()]
        public void ToStringFromStringTest()
        {
            Settings test = new Settings();

            test.TimeEntries.Add(new TimeEntry("Test Project", "Test Task1", "Test Employer", DateTime.Now));
            TimeEntry temp = test.LastUnclosedTask;
            test.TimeEntries.Remove(temp);
            temp.Stop = DateTime.Now;
            test.TimeEntries.Add(temp);
            test.TimeEntries.Add(new TimeEntry("Test Project", "Test Task2", "Test Employer", DateTime.Now.AddDays(-1)));

            string tempFile = Path.GetTempFileName();

            File.WriteAllText(tempFile, test.ToString());

            Settings fromFile = new Settings(tempFile);

            Assert.AreEqual(test.ToString(), fromFile.ToString());
        }

        [TestMethod()]
        public void SettingsTest()
        {
            Settings test = new Settings();

            Assert.AreEqual(0, test.CommonTasks.Count);
            Assert.AreEqual(0, test.TimeEntries.Count);
            Assert.AreEqual(15, test.StillWorkingTime);
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
            Assert.AreEqual("Test Employer", temp.Employer);

            test.TimeEntries.Remove(temp);

            Assert.AreEqual(null, test.LastUnclosedTask);

            temp.Stop = DateTime.Now;

            test.TimeEntries.Add(temp);

            Assert.AreEqual(null, test.LastUnclosedTask);

            test.TimeEntries.Add(new TimeEntry("Test Project", "Test Task2", "Test Employer", DateTime.Now.AddDays(-1)));

            temp = test.LastUnclosedTask;

            Assert.AreEqual("Test Project", temp.Project);
            Assert.AreEqual("Test Task2", temp.Task);
            Assert.AreEqual("Test Employer", temp.Employer);
        }
    }
}