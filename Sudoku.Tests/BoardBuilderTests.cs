using System.Linq;
using NUnit.Framework;
using Sudoku.Domain.Builders;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Parts;

namespace Sudoku.Tests
{
    [TestFixture]
    public class BoardBuilderTests
    {
        private BoardBuilder boardBuilder;

        private static object[] squares =
        {
            new object[] { new SquareLeaf(false, "0", new Coordinate(0, 0)) },
            new object[] { new SquareLeaf(true, "1", new Coordinate(0,0)) },
            new object[] { new SquareLeaf(true, new Coordinate(0,0)) }
        };

        [SetUp]
        public void SetUp()
        {
            boardBuilder = new BoardBuilder();
        }

        [TestCase(false)]
        [TestCase(true)]
        public void BuildDivider_WithBool_ReturnsListWithDivider(bool horizontal)
        {
            boardBuilder.BuildDivider(horizontal);

            var product = boardBuilder.GetResult();

            Assert.That(product.parts.Count(p => p.GetType() == typeof(Divider)), Is.EqualTo(1));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void BuildDivider_WithBool_ReturnsDividerCorrectOrientation(bool horizontal)
        {
            boardBuilder.BuildDivider(horizontal);

            var product = boardBuilder.GetResult();
            var divider = product.parts.Cast<Divider>().First();

            Assert.AreEqual(divider.Horizontal, horizontal);
        }

        [TestCase(null)]
        [TestCase(1)]
        [TestCase(2)]
        public void BuildSpacer_WithSize_ReturnsListWithSpacer(int size)
        {
            boardBuilder.BuildSpacer(size);

            var product = boardBuilder.GetResult();

            Assert.That(product.parts.Count(p => p.GetType() == typeof(Spacer)), Is.EqualTo(1));
        }

        [TestCase(null)]
        [TestCase(1)]
        [TestCase(2)]
        public void BuildSpacer_WithSize_ReturnsSpacerCorrectWidth(int size)
        {
            boardBuilder.BuildSpacer(size);

            var product = boardBuilder.GetResult();
            var spacer = product.parts.Cast<Spacer>().First();

            Assert.AreEqual(spacer.Size, size);
        }

        [TestCaseSource(nameof(squares))]
        public void BuildSquare_WithSquare_ReturnsListWithSquare(SquareLeaf leaf)
        {
            boardBuilder.BuildSquare(leaf);

            var product = boardBuilder.GetResult();

            Assert.That(product.parts.Count(p => p.GetType() == typeof(Square)), Is.EqualTo(1));
        }

        [TestCaseSource(nameof(squares))]
        public void Builder_BuildSquare_ReturnsSquareCorrectData(SquareLeaf square)
        {
            boardBuilder.BuildSquare(square);

            var product = boardBuilder.GetResult();
            var buildSquare = product.parts.Cast<Square>().First();

            Assert.AreEqual(square.Value, buildSquare.SquareLeaf.Value);
            Assert.AreEqual(square.IsLocked, buildSquare.SquareLeaf.IsLocked);
            Assert.AreEqual(square.Coordinate.X, buildSquare.SquareLeaf.Coordinate.X);
            Assert.AreEqual(square.Coordinate.Y, buildSquare.SquareLeaf.Coordinate.Y);
        }

        [Test]
        public void Builder_BuildRow_ReturnsListWithRow()
        {
            boardBuilder.BuildRow();

            var product = boardBuilder.GetResult();

            Assert.That(product.parts.Count(p => p.GetType() == typeof(Row)), Is.EqualTo(1));
        }
    }
}