#nullable enable

using System;
using System.Collections.Generic;
using Sudoku.Domain.Models;

namespace Sudoku.Domain.Utilities
{
    public class DistinctLeafComparer : IEqualityComparer<CellLeaf>
    {
        public bool Equals(CellLeaf? x, CellLeaf? y)
        {
            return x!.Position.Y == y!.Position.Y && x!.Position.X == y!.Position.X;
        }

        public int GetHashCode(CellLeaf obj)
        {
            return HashCode.Combine(obj.IsLocked, obj.IsSelected, obj.Position);
        }
    }
}