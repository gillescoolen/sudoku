using System.IO;
using Sudoku.Domain.Factories;
using Sudoku.Domain.Models;

namespace Sudoku.Parser
{
    public class SudokuParser : ISudokuParser
    {
        private readonly SudokuFactory sudokuFactory;

        public SudokuParser()
        {
            sudokuFactory = new SudokuFactory();
            sudokuFactory.AddSudokuFactory("4x4", typeof(SudokuNormalFactory));
            sudokuFactory.AddSudokuFactory("6x6", typeof(SudokuNormalFactory));
            sudokuFactory.AddSudokuFactory("9x9", typeof(SudokuNormalFactory));
        }

        public SudokuWrapper Parse(string type)
        {
            var sudokuFactory = this.sudokuFactory.CreateSudokuFactory(type);
            return sudokuFactory.CreateSudoku(File.ReadAllText($"./Sudoku.Terminal/Formats/puzzle.{type}"));
        }
    }
}