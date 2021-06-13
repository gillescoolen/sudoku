using Sudoku.Domain.Factories;
using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Domain.Services
{
    public class FactoryService : IFactoryService
    {
        private readonly SudokuFactory factory;

        public FactoryService()
        {
            factory = new SudokuFactory();

            factory.AddSudokuFactory("4x4", typeof(SudokuNormalFactory));
            factory.AddSudokuFactory("6x6", typeof(SudokuNormalFactory));
            factory.AddSudokuFactory("9x9", typeof(SudokuNormalFactory));
        }

        public BaseSudoku Create(string format, string data)
        {
            var sudokuFactory = factory.CreateSudokuFactory(format);
            return sudokuFactory?.CreateSudoku(data)!;
        }
    }
}