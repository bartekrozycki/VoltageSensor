using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using VoltageSensor.Models;
using VoltageSensor.Models.VoltageSensor;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private readonly SensorMath math = new SensorMath();
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod1()
        {
            math.regression(null, DateTime.Now);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod2()
        {
            List<Sensor> l = new List<Sensor>();
            math.regression(l, DateTime.Now);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod3()
        {
            List<Sensor> l = new List<Sensor>();
            l.Add(new Sensor
            {
                CurrentVoltage = 1,
                error = false,
                TimeStamp = DateTime.Now
            });

            math.regression(l, DateTime.Now);
        }
        [TestMethod]
        public void TestMethod5()
        {
            List<Sensor> l = new List<Sensor>();
            DateTime now = DateTime.Now;

            for (int i = 0; i < 10; i++)
            {
                l.Add(new Sensor
                {
                    CurrentVoltage = i,
                    error = false,
                    TimeStamp = now.AddSeconds(i)
                });
            }

            Assert.IsTrue((math.regression(l, now.AddSeconds(10)) - 10) <= 0.00001);
            Assert.IsTrue((math.regression(l, now.AddSeconds(25)) - 25) <= 0.00001);
            Assert.IsTrue((math.regression(l, now.AddSeconds(100)) - 100) <= 0.00001);
        }
    }
}
