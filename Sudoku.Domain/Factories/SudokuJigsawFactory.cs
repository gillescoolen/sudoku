﻿using System.Collections.Generic;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Domain.Factories
{
    public class SudokuJigsawFactory : IAbstractSudokuFactory
    {
        public SudokuWrapper CreateSudoku(string data)
        {
            return new NumberJigsawSudoku(new List<IComponent> { new CellLeaf(true, new Position(0, 0)) });
        }
    }
}