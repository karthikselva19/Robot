using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCleaner.Tests
{
    class TestReporter : IReport
    {

        public string PrintReport()
        {
            return "=> Cleaned: 0";
        }


        public void RegisterNewPosition(Location position)
        {
        }
    }
}
