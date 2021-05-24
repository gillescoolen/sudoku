using System;
using Sudoku.Domain.Models;

namespace Sudoku.Domain
{
    public class Game
    {
        private Cell[] Cells { get; set; }
        public Game(Cell[] cells)
        {
            Cells = cells;
        }
    }
}
