using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Domain.Models
{
    public class Cell
    {

        private int DefinitiveValue { get; set; }
        private int HintValue { get; set; }
        private Position Position { get; set; }

        public Cell(int h, int d, Position p) {
            DefinitiveValue = d;
            HintValue = h;
            Position = p;
        }

        public bool IsValid()
        {
            return true;
        }

        public int GetValue()
        {
            return DefinitiveValue;
        }

        public void SetValue(int value)
        {
            DefinitiveValue = value;
        }
    }
}
