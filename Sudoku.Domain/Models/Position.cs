using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Domain.Models
{
    class Position
    {
        private int X { get; set; }
        private int Y { get; set; }
        private int BoxIndex { get; set; }

        public Position(int x, int y, int boxIndex) {
            X = x;
            Y = y;
            BoxIndex = boxIndex;
        }
    }
}
