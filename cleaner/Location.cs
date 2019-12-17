using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotCleaner
{
    public class Location : IComparable<Location>
    {
        private int _x;
        private int _y;

        public Location(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public int X
        {
            get { return _x; }
        }

        public int Y
        {
            get
            {
                return _y;
            }
        }

        internal void Validate(Location bottomLeftBound, Location topRightBound)
        {
            if (bottomLeftBound != null)
            {
                if (_x < bottomLeftBound.X) _x = bottomLeftBound.X;
                if (_y < bottomLeftBound.Y) _y = bottomLeftBound.Y;
            }
            if (topRightBound != null)
            {
                if (_x > topRightBound.X) _x = topRightBound.X;
                if (_y > topRightBound.Y) _y = topRightBound.Y;
            }

        }

        public override string ToString()
        {
            return _x + "$" + _y;
        }


        public int CompareTo(Location other)
        {
            if (this.X == other.X)
                return this.Y.CompareTo(other.Y);
            return this.X.CompareTo(other.X);
        }
    }
}
