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

        public override bool Equals(object? data)
        {
            if (data == null || GetType() != data.GetType())
            {
                return false;
            }

            var coordinate = (Coordinate)data;
            return Equals(coordinate);
        }

        protected bool Equals(Coordinate other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}