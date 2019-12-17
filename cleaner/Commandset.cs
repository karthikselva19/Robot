using System.Collections.Generic;

namespace RobotCleaner
{
    public class CommandSet
    {
        public int NumberOfCommands
        {
            get { return _numberOfCommands; }
            set { _numberOfCommands = value; }
        }
        private int _numberOfCommands;

        public Location StartPosition
        {
            get { return _startPosition; }
            set { _startPosition = value; }
        }

        private Location _startPosition;

        public List<MovementCommand> MovementCommands
        {
            get { return _movementCommands; }
        }
        private List<MovementCommand> _movementCommands;

        public CommandSet()
        {
            _movementCommands = new List<MovementCommand>();
        }
    }
}