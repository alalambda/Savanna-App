using System;
using System.Collections.Generic;
using System.Text;

namespace SavannaApp.Model
{
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object value)
        {
            Coordinates coordinates = value as Coordinates;

            return (coordinates != null)
                && (X == coordinates.X)
                && (Y == coordinates.Y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
