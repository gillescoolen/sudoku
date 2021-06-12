using Sudoku.Domain.Factories;
using Sudoku.Domain.Models;

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
            factory.AddSudokuFactory("Jigsaw", typeof(SudokuJigsawFactory));
            factory.AddSudokuFactory("Samurai", typeof(SudokuSamuraiFactory));
        }

        public BaseSudoku Create(string type, string data)
        {
            var sudokuFactory = factory.CreateSudokuFactory(type);
            return sudokuFactory?.CreateSudoku(data)!;
        }
    }
}