using System.Collections.Generic;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Models
{
    public class Board
    {
        public List<IPart> parts { get; } = new();

        public void Add(IPart part)
        {
            parts.Add(part);
        }
    }
}