using System.Collections.Generic;

namespace RobotCleaner
{
    public class CommandFactory
    {
        private const int MaxNumberSteps = 99999;
        private const int MinNumberSteps = 1;

        private List<string> _inputStrings;

        internal readonly CommandSet _commandSet;

        public CommandFactory()
        {
            _inputStrings = new List<string>();
            _commandSet = new CommandSet();
        }

        public void AddInput(string inputString)
        {
            if (!InputsAreComplete)
            {
                if (_inputStrings.Count == 0)
                {
                    SetNumberOfCommands(inputString);
                }
                else if (_inputStrings.Count == 1)
                {
                    SetStartPosition(inputString);
                }
                else
                {
                    _commandSet.MovementCommands.Add(ParseMovementCommand(inputString));
                }
                _inputStrings.Add(inputString);
            }
        }

        private MovementCommand ParseMovementCommand(string inputString)
        {
            MovementCommand moveCommand = new MovementCommand();

            string[] movementInputBits = inputString.Split(null);

            if (movementInputBits.Length > 1)
            {
                switch (movementInputBits[0].ToUpper())
                {
                    case "N":
                        moveCommand.MoveDirection = Direction.North;
                        break;
                    case "S":
                        moveCommand.MoveDirection = Direction.South;
                        break;
                    case "E":
                        moveCommand.MoveDirection = Direction.East;
                        break;
                    case "W":
                        moveCommand.MoveDirection = Direction.West;
                        break;
                    default:
                        moveCommand.MoveDirection = Direction.Unknown;
                        break;
                }

                moveCommand.MoveSteps = int.Parse(movementInputBits[1]);
                if (moveCommand.MoveSteps > MaxNumberSteps)
                    moveCommand.MoveSteps = MaxNumberSteps;
                if (moveCommand.MoveSteps < MinNumberSteps)
                    moveCommand.MoveSteps = MinNumberSteps;
            }

            return moveCommand;
        }

        private void SetStartPosition(string inputString)
        {
            string[] coordinateStringBits = inputString.Split(null);
            if (coordinateStringBits.Length > 1)
            {
                int x = int.Parse(coordinateStringBits[0]);
                int y = int.Parse(coordinateStringBits[1]);
                _commandSet.StartPosition = new Location(x,y);
            }
        }

        private void SetNumberOfCommands(string inputString)
        {
            _commandSet.NumberOfCommands = int.Parse(inputString);
            if (_commandSet.NumberOfCommands < 0)
                _commandSet.NumberOfCommands = 0;
            if (_commandSet.NumberOfCommands > 10000)
                _commandSet.NumberOfCommands = 10000;
        }

        public bool InputsAreComplete
        {
            get { return _inputStrings.Count == (_commandSet.NumberOfCommands + 2); }
        }

   

        public CommandSet GetCommandSet()
        {
            if (InputsAreComplete)
                return _commandSet;

            return null;
        }
    }
}