#nullable enable

using System;

namespace Sudoku.Domain.Models
{
    public class Coordinate
    {
        public int X { get; }
        public int Y { get; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var p = (Coordinate)obj;
            return Equals(p);
        }

        protected bool Equals(Coordinate other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}