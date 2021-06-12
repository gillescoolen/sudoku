#nullable enable

using System;
using System.Collections.Generic;
using Sudoku.Domain.Models;

namespace Sudoku.Domain.Utilities
{
    public class DistinctLeafComparer : IEqualityComparer<SquareLeaf>
    {
        public bool Equals(SquareLeaf? x, SquareLeaf? y)
        {
            return x!.Coordinate.Y == y!.Coordinate.Y && x!.Coordinate.X == y!.Coordinate.X;
        }

        public int GetHashCode(SquareLeaf obj)
        {
            return HashCode.Combine(obj.IsLocked, obj.IsSelected, obj.Coordinate);
        }
    }
}