using Moq;
using NUnit.Framework;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.States;
using Sudoku.Tests.TestData;

namespace Sudoku.Tests
{
    [TestFixture]
    public class HintStateTests
    {
        private SquareLeaf square;
        private BaseSudoku sudoku;

        [SetUp]
        public void Setup()
        {
            square = new SquareLeaf(false, "0", new Coordinate(0, 0)) { HintValue = "8" };
            sudoku = CorrectBaseSudokuData.BaseSudoku;
        }

        [Test]
        public void GetValueFromSquare_SquareWithValue_SetsNewValue()
        {
            var state = new HintState();

            state.EnterValue("2", square);

            Assert.AreEqual("2", square.HintValue);
        }

        [Test]
        public void CheckEqualityOfSquares_EqualSquares_ReturnsTrue()
        {
            var squareTwo = new SquareLeaf(false, "0", new Coordinate(0, 0)) { HintValue = "8" };
            var state = new HintState();

            var resultOne = state.CheckEquality(square, squareTwo);
            var resultTwo = state.CheckEquality(square, squareTwo);

            Assert.IsTrue(resultOne);
            Assert.IsTrue(resultTwo);
        }

        [Test]
        public void CheckEqualityOfSquares_UnEqualSquares_ReturnsFalse()
        {
            var squareTwo = new SquareLeaf(false, "0", new Coordinate(0, 0)) { HintValue = "9" };
            var state = new HintState();

            var resultOne = state.CheckEquality(square, squareTwo);
            var resultTwo = state.CheckEquality(square, squareTwo);

            Assert.IsFalse(resultOne);
            Assert.IsFalse(resultTwo);
        }

        [Test]
        public void SquareHasValue_WithValue_ReturnsTrue()
        {
            var state = new HintState();

            var result = state.HasSquareValue(square);

            Assert.IsTrue(result);
        }

        [Test]
        public void SquareHasValue_WithoutValue_ReturnsFalse()
        {
            square.HintValue = "0";
            var state = new HintState();

            var result = state.HasSquareValue(square);

            Assert.IsFalse(result);
        }

        [Test]
        public void ConstructsField_WithValidBaseSudoku_ReturnsField()
        {
            var state = new HintState();
            var context = new Mock<IContext>();
            context.Setup(c => c.Sudoku()).Returns(sudoku);

            state.SetContext(context.Object);
            var result = state.Construct();

            Assert.IsNotNull(result);
        }
    }
}