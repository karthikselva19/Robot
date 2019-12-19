using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RobotCleaner.Tests
{
    [TestClass]
    public class CommandFactoryTests
    {
        [TestMethod]
        public void CreateCommandFactory_AddTwoInputCommands_InputsAreComplete()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();

            //act
            commandFactory.AddInput("2");
            commandFactory.AddInput("10 22");
            commandFactory.AddInput("E 2");
            commandFactory.AddInput("N 1");

            //assert
            Assert.IsTrue(commandFactory.InputsAreComplete);
        }

        [TestMethod]
        public void CreateCommandFactory_AddThreeInputCommands_InputsAreComplete()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();

            //act
            commandFactory.AddInput("3");
            commandFactory.AddInput("10 22");
            commandFactory.AddInput("E 2");
            commandFactory.AddInput("N 1");
            commandFactory.AddInput("S 1");

            //assert
            Assert.IsTrue(commandFactory.InputsAreComplete);
        }


        [TestMethod]
        public void CreateCommandFactory_AddZeroInputCommands_InputsAreComplete()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();

            //act
            commandFactory.AddInput("0");
            commandFactory.AddInput("10 22");

            //assert
            Assert.IsTrue(commandFactory.InputsAreComplete);
        }


        [TestMethod]
        public void CreateCommandFactory_AddTenThousandInputCommands_InputsAreComplete()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();

            //act
            commandFactory.AddInput("10000");
            commandFactory.AddInput("10 22");
            for (int i = 0; i < 10000; i++)
            {
                commandFactory.AddInput("E1");
            }

            //assert
            Assert.IsTrue(commandFactory.InputsAreComplete);
        }

        [TestMethod]
        public void CreateCommandFactory_AddMinusInputCommands_InputsAreComplete()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();

            //act
            commandFactory.AddInput("-3");
            commandFactory.AddInput("10 22");
            for (int i = 0; i < 10000; i++)
            {
                commandFactory.AddInput("E1");
            }

            //assert
            Assert.IsTrue(commandFactory.InputsAreComplete);
        }

        [TestMethod]
        public void CreateCommandFactory_AddTwentyThousandInputCommands_InputsAreComplete()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();

            //act
            commandFactory.AddInput("11000");
            commandFactory.AddInput("10 22");
            for (int i = 0; i < 20000; i++)
            {
                commandFactory.AddInput("E1");
            }

            //assert
            Assert.IsTrue(commandFactory.InputsAreComplete);
        }


        [TestMethod]
        public void CreateCommandFactory_NormalPosition_PositionIsCorrect()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();

            //act
            commandFactory.AddInput("0");
            commandFactory.AddInput("10 22");

            //assert
            Assert.AreEqual(10, commandFactory._commandSet.StartPosition.X);
            Assert.AreEqual(22, commandFactory._commandSet.StartPosition.Y);
        }

        [TestMethod]
        public void CreateCommandFactory_TabSeparator_PositionIsCorrect()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();

            //act
            commandFactory.AddInput("0");
            commandFactory.AddInput("10\t22");

            //assert
            Assert.AreEqual(10, commandFactory._commandSet.StartPosition.X);
            Assert.AreEqual(22, commandFactory._commandSet.StartPosition.Y);
        }


        [TestMethod]
        public void CreateCommandFactory_OneMovementCommandInput_CorrectNumberMovementCommandsAdded()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();

            //act
            commandFactory.AddInput("3");
            commandFactory.AddInput("10 22");
            commandFactory.AddInput("E 2");

            //assert
            Assert.AreEqual(1,commandFactory._commandSet.MovementCommands.Count);
        }

        [TestMethod]
        public void CreateCommandFactory_TooManySteps_StepsShouldBe99999()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();

            //act
            commandFactory.AddInput("1");
            commandFactory.AddInput("10 22");
            commandFactory.AddInput("E 200000");
            MovementCommand movementCommand = commandFactory._commandSet.MovementCommands[0];

            //assert
            Assert.AreEqual(99999, movementCommand.MoveSteps);
        }

        [TestMethod]
        public void CreateCommandFactory_TooFewSteps_StepsShouldBe1()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();

            //act
            commandFactory.AddInput("1");
            commandFactory.AddInput("10 22");
            commandFactory.AddInput("E -3");
            MovementCommand movementCommand = commandFactory._commandSet.MovementCommands[0];

            //assert
            Assert.AreEqual(1, movementCommand.MoveSteps);
        }


        [TestMethod]
        public void CommandFactory_AddInCompleteInputs_NullCommandSet()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();

            //act
            commandFactory.AddInput("5");
            commandFactory.AddInput("10 22");
            commandFactory.AddInput("E -3");
            CommandSet commandSet = commandFactory.GetCommandSet();

            //assert
            Assert.IsNull(commandSet);
        }


        [TestMethod]
        public void CommandFactory_AddCompleteInputs_NotNullCommandSet()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();

            //act
            commandFactory.AddInput("5");
            commandFactory.AddInput("10 22");
            commandFactory.AddInput("E -3");
            commandFactory.AddInput("E -3");
            commandFactory.AddInput("E -3");
            commandFactory.AddInput("E -3");
            commandFactory.AddInput("E -3");
            CommandSet commandSet = commandFactory.GetCommandSet();

            //assert
            Assert.IsNotNull(commandSet);
        }

        [TestMethod]
        public void CommandFactory_Add5Command_NotNullCommandSetHas5Commands()
        {
            //arrange
            CommandFactory commandFactory = new CommandFactory();

            //act
            commandFactory.AddInput("5");
            commandFactory.AddInput("10 22");
            commandFactory.AddInput("E -3");
            commandFactory.AddInput("E -3");
            commandFactory.AddInput("E -3");
            commandFactory.AddInput("E -3");
            commandFactory.AddInput("E -3");
            CommandSet commandSet = commandFactory.GetCommandSet();

            //assert
           Assert.AreEqual(5, commandSet.MovementCommands.Count);
        }
    }

           
 
}
