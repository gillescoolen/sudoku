﻿namespace Sudoku.Domain.Models.Interfaces
{
    public interface IVisitor
    {
        public Board Visit(SudokuWrapper sudoku);
    }
}