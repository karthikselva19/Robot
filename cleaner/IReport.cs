using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotCleaner
{
    public interface IReport
    {
        string PrintReport();

        void RegisterNewPosition(Location position);
    }
}
