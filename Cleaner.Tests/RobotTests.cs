using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RobotCleaner.Tests
{
    [TestClass]
    public class RobotTests
    {
        [TestMethod]
        public void CreateRobot_RobotCreated()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();
            commandFactory.AddInput("0");
            commandFactory.AddInput("0 0");
            CommandSet commandSet = commandFactory.GetCommandSet();

            //act
            Robot robot = new Robot(commandSet, null);

            //assert
            Assert.IsNotNull(robot);
        }

        [TestMethod]
        public void RunRobot_EmptyCommandSet_RobotPositionShouldNotChange()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();
            commandFactory.AddInput("0");
            commandFactory.AddInput("0 0");
            CommandSet commandSet = commandFactory.GetCommandSet();
            Robot robot = new Robot(commandSet, null);

            //act
            robot.ExecuteCommands();

            //assert
            Assert.AreEqual(commandSet.StartPosition.X, robot.Position.X);
            Assert.AreEqual(commandSet.StartPosition.Y, robot.Position.Y);
        }

        [TestMethod]
        public void RunRobot_SimpleCommandSet_RobotPositionYShouldChangeBy1()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();
            commandFactory.AddInput("1");
            commandFactory.AddInput("0 0");
            commandFactory.AddInput("N 1");
            CommandSet commandSet = commandFactory.GetCommandSet();
            Robot robot = new Robot(commandSet, null);

            //act
            robot.ExecuteCommands();

            //assert
            Assert.AreEqual(commandSet.StartPosition.X, robot.Position.X);
            Assert.AreEqual(commandSet.StartPosition.Y + 1, robot.Position.Y);
        }


        [TestMethod]
        public void RunRobot_IllegalOutOfBoundsCommandSet_RobotPositionShouldRemainWithinBounds()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();
            commandFactory.AddInput("1");
            commandFactory.AddInput("100000 100000");
            commandFactory.AddInput("N 1");
            CommandSet commandSet = commandFactory.GetCommandSet();
            Robot robot = new Robot(commandSet, null, null, new Location(100000, 100000) );

            //act
            robot.ExecuteCommands();

            //assert
            Assert.AreEqual(commandSet.StartPosition.X, robot.Position.X);
            Assert.AreEqual(commandSet.StartPosition.Y, robot.Position.Y);
        }
        [TestMethod]
        public void RunRobot_EmptyCommandSet_ZeroPlaceReportGenerated()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();
            commandFactory.AddInput("0");
            commandFactory.AddInput("0 0");
            IReport reporter = new TestReporter();
            CommandSet commandSet = commandFactory.GetCommandSet();
            Robot robot = new Robot(commandSet, reporter);


            //act
            robot.ExecuteCommands();
            string report = robot.PrintReport();

            //assert
            Assert.AreEqual("=> Cleaned: 0", report);
        }

        [TestMethod]
        public void RunRobot_SimplestCommandSetOneMove_RobotReports1LocationCleaned()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();
            commandFactory.AddInput("1");
            commandFactory.AddInput("10 10");
            commandFactory.AddInput("N 1");
            CommandSet commandSet = commandFactory.GetCommandSet();
            IReport reporter = new SimpleReporter();
            Robot robot = new Robot(commandSet, reporter, new Location(-100000, -100000), new Location(100000, 100000));

            robot.ExecuteCommands();

            //act
            string report = robot.PrintReport();

            //assert
            Assert.AreEqual("=> Cleaned: 1", report);
        }
        [TestMethod]
        public void RunRobot_EmptyCommandSetNullReporter_ZeroPlaceReportGenerated()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();
            commandFactory.AddInput("0");
            commandFactory.AddInput("0 0");
            CommandSet commandSet = commandFactory.GetCommandSet();
            Robot robot = new Robot(commandSet, null);


            //act
            robot.ExecuteCommands();
            string report = robot.PrintReport();

            //assert
            Assert.AreEqual("=> Cleaned: unknown", report);
        }


        [TestMethod]
        public void RunRobot_BoxMoveCommandSet_NumberofLocationsIsCorrect()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();
            commandFactory.AddInput("4");
            commandFactory.AddInput("0 0");
            commandFactory.AddInput("N 7");
            commandFactory.AddInput("E 7");
            commandFactory.AddInput("S 7");
            commandFactory.AddInput("W 7");
            CommandSet commandSet = commandFactory.GetCommandSet();
            IReport reporter = new SimpleReporter();
            Robot robot = new Robot(commandSet, reporter, new Location(0, 0), new Location(7, 7));

            //act
            robot.ExecuteCommands();
            string report = robot.PrintReport();

            //assert
            Assert.AreEqual("=> Cleaned: 28", report);
        }
    }
}
