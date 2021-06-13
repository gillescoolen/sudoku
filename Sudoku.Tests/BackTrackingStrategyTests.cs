using System.Linq;
using NUnit.Framework;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.States;
using Sudoku.Domain.Strategies;
using Sudoku.Tests.TestData;

namespace Sudoku.Tests
{
    [TestFixture]
    public class BackTrackingStrategyTests
    {
        private BaseSudoku sudoku;

        [SetUp]
        public void Setup()
        {
            sudoku = CorrectBaseSudokuData.BaseSudoku;
        }

        [Test]
        public void SolveBaseSudoku_ValidBaseSudokuWithDefinitiveModeState_NoEmptySquares()
        {
            var strategy = new BackTrackStrategy();

            var success = strategy.Solve(sudoku, new DefinitiveState());
            var count = sudoku.Sudokus
                .SelectMany(quadrant => quadrant.Find(leaf => !leaf.IsComposite()))
                .Cast<SquareLeaf>()
                .Count(c => c.Value.Equals("0"));

            Assert.AreEqual(0, count);
            Assert.IsTrue(success);
        }

        [Test]
        public void SolveSudoku_ValidSudokuWithHintState_Invalid()
        {
            var strategy = new BackTrackStrategy();

            strategy.Solve(sudoku, new HintState());

            var doubles = sudoku.Sudokus.SelectMany(quadrant => quadrant.Find(leaf => !leaf.IsComposite()))
                .Cast<SquareLeaf>().GroupBy(g => g.Value).Where(g => g.Count() > 3).Select(g => g.Key);

            Assert.IsTrue(doubles.Any());
        }

        [Test]
        public void SolveSudoku_InvalidSudokuWithDefinitiveState()
        {
            var strategy = new BackTrackStrategy();

            sudoku.Sudokus
                .SelectMany(quadrant => quadrant.Find(square => !square.IsComposite()))
                .Cast<SquareLeaf>().First().Value = "4";

            var success = strategy.Solve(sudoku, new DefinitiveState());

            Assert.IsFalse(success);
        }
    }
}