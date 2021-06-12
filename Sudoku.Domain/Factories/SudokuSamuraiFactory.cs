using System;
using Sudoku.Domain.Models;

namespace Sudoku.Domain.Factories
{
    public class SudokuSamuraiFactory : SudokuNormalFactory
    {

        public override BaseSudoku CreateSudoku(string data)
        {
            throw new NotImplementedException();
        }
    }
}