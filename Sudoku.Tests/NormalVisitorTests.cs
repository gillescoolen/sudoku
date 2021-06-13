using System.Linq;
using NUnit.Framework;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Models.Parts;
using Sudoku.Domain.Visitors;
using Sudoku.Tests.TestData;

namespace Sudoku.Tests
{
    [TestFixture]
    public class NormalVisitorTests
    {
        private BaseSudoku sudoku;

        [SetUp]
        public void Setup()
        {
            sudoku = CorrectBaseSudokuData.BaseSudoku;
        }

        [Test]
        public void GetField_WithNormalSudoku_ExpectParts()
        {
            var visitor = new NormalSudokuVisitor();

            var field = visitor.Visit(sudoku);
            var squares = field.parts.Where(p => p is Square).ToList();

            Assert.AreEqual(31, field.parts.Count);
            Assert.AreEqual(16, squares.Count);
        }
    }
}