using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RobotCleaner
{
    public class Robot
    {

        public Location Position { get; set; }
        private CommandSet _commandSet;
        private readonly IReport _reporter;
        private readonly Location _bottomLeftBound;
        private readonly Location _topRightBound;

        public Robot(CommandSet commandSet, IReport reporter) : this(commandSet, reporter, null, null)
        { 
        }


        public Robot(CommandSet commandSet, IReport reporter, Location bottomLeftBound, Location topRightBound)
        {
            _commandSet = commandSet;
            _reporter = reporter;
            _bottomLeftBound = bottomLeftBound;
            _topRightBound = topRightBound;
            Position = _commandSet.StartPosition;
        }

        public void ExecuteCommands()
        {
            foreach (MovementCommand move in _commandSet.MovementCommands)
            {
                for (int i = 0; i < move.MoveSteps; i++)
                {
                    MoveRobot(move);
                }
            }
        }

        private void MoveRobot(MovementCommand move)
        {
            switch (move.MoveDirection)
            {
                case Direction.North:
                    Position = new Location(Position.X, Position.Y + 1);
                    break;
                case Direction.East:
                    Position = new Location(Position.X + 1, Position.Y);
                    break;
                case Direction.South:
                    Position = new Location(Position.X, Position.Y - 1);
                    break;
                case Direction.West:
                    Position = new Location(Position.X - 1, Position.Y);
                    break;
            }

            Position.Validate(_bottomLeftBound, _topRightBound);

            if (_reporter != null) _reporter.RegisterNewPosition(Position);
 
        }


        public string PrintReport()
        {
            if (_reporter == null)
                return "=> Cleaned: unknown";

            return _reporter.PrintReport();
        }
    }
}
