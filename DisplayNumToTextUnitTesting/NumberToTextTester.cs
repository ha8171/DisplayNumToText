using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DisplayNumToText.Processor;
using DisplayNumToText.Logger;
using System.Configuration;

namespace DisplayNumToTextUnitTesting
{
    [TestClass]
    public class NumberToTextTester
    {
        [TestInitialize]
        public void TestInitialize()
        {
            ConfigurationManager.AppSettings["LogFileDir"] = @"E:\DisplayNumToText\";
            ConfigurationManager.AppSettings["LogFileName"] = "LogFile.txt";
        }
        /// <summary>
        /// It validates the process method that returns the converted text
        /// </summary>
        [TestMethod]
        public void TestProcess()
        {
            NumberToText<int> numToText = new NumberToText<int>();
            numToText.SetValues(10, 100, 20, 50);
            var message = numToText.Process();
            var expected = "Output - \n 10: 10 \n 20: Fancy \n 50: Pants \n 100: Fancy Pants \n";
            Assert.AreEqual(expected.Replace(" ", "").Replace("\n", ""), message.Replace(" ", "").Replace(Environment.NewLine, ""));
        }

        /// <summary>
        /// It validates the inputs and returns error message for Low field
        /// </summary>
        [TestMethod]
        public void TestProcessLowErrorMessage()
        {
            NumberToText<int> numToText = new NumberToText<int>();
            numToText.SetValues(10, 100, 5, 50);
            var message = numToText.Process();
            var expected = "Error: Low should be lesser value";
            Assert.AreEqual(expected, message.Replace(Environment.NewLine, ""));
        }

        /// <summary>
        /// It validates the inputs and returns error message for High field
        /// </summary>
        [TestMethod]
        public void TestProcessHighErrorMessage()
        {
            NumberToText<int> numToText = new NumberToText<int>();
            numToText.SetValues(5, 10, 10, 50);
            var message = numToText.Process();
            var expected = "Error: High should be higher value";
            Assert.AreEqual(expected, message.Replace(Environment.NewLine, ""));
        }

        /// <summary>
        /// It validates the inputs returns error message for High field
        /// </summary>
        [TestMethod]
        public void TestProcessNegativeIntegers()
        {
            NumberToText<int> numToText = new NumberToText<int>();
            numToText.SetValues(-10, 100, 50, 20);
            var message = numToText.Process();
            var expected = "Error: Input should be > 0";
            Assert.AreEqual(expected, message.Replace(Environment.NewLine, ""));
        }
    }
}
