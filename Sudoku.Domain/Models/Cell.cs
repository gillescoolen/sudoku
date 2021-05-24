using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Domain.Models
{
    class Cell
    {
        private int DefinitiveValue { get; set; }
        private int HintValue { get; set; }
        private Position Position { get; set; }

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
