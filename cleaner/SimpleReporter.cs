using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotCleaner
{
    public class SimpleReporter: IReport  
    {
        //private readonly List<string> _cleanedLocationStrings;
        private SortedSet<Location> _cleanedLocationStrings;
        //"-5000$-7000"


        public SimpleReporter()
        {
            _cleanedLocationStrings = new SortedSet<Location>();
        }

        public string PrintReport()
        {
            return string.Format("=> Cleaned: {0}", _cleanedLocationStrings.Count);
        }


        public void RegisterNewPosition(Location position)
        {
            _cleanedLocationStrings.Add(position);
        }
    }
}
