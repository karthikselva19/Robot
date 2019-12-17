using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCleaner
{
    public class MovementCommand
    {
        internal Direction MoveDirection { get; set; }
        internal int MoveSteps { get; set; }
    }
}
