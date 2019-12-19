using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RobotCleaner.Tests
{
    /// <summary>
    /// Summary description for ReporterTests
    /// </summary>
    [TestClass]
    public class ReporterTests
    {
        public ReporterTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void RunRobot_SomeCasesAndManyCases_NLogNPerformance()
        {
            SimpleReporter reporter = new SimpleReporter();

            //Arrange
            Location currentPosition = new Location(0, 0);
            var timer = Stopwatch.StartNew();
            for (int i = 1; i < 2300; i++)
            {
                currentPosition = new Location(currentPosition.X, currentPosition.Y + 1);
                reporter.RegisterNewPosition(currentPosition);
            }
            timer.Stop();
            var elapsedTime23Cases = timer.Elapsed;


            //Act
            timer = Stopwatch.StartNew();
            for (int i = 1; i < 9200; i++)
            {
                currentPosition = new Location(currentPosition.X, currentPosition.Y + 1);
                reporter.RegisterNewPosition(currentPosition);
            }
            timer.Stop();
            var elapsedTime92Cases = timer.Elapsed;

            //Assert
            Assert.IsTrue(elapsedTime92Cases.Milliseconds < (elapsedTime23Cases.Milliseconds * 8), elapsedTime92Cases.Milliseconds + "vs"+ (elapsedTime23Cases.Milliseconds * 8));
        }
    }
}
