using Moq;
using NUnit.Framework;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Parts;

namespace Sudoku.Tests
{
    [TestFixture]
    public class PartVisitTests
    {
        private Mock<IPrintVisitor> PrintVisitor;

        private static object[] parts =
        {
            new object[] {new Row()},
            new object[] {new Spacer(1)},
            new object[] {new Divider(true)},
            new object[] {new Divider(false)},
            new object[] {new Square(new SquareLeaf(true, new Coordinate(0,0)))},
        };

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            PrintVisitor = new Mock<IPrintVisitor>();

            PrintVisitor.Setup(visitor => visitor.Visit(It.IsAny<Row>())).Verifiable();
            PrintVisitor.Setup(visitor => visitor.Visit(It.IsAny<Square>())).Verifiable();
            PrintVisitor.Setup(visitor => visitor.Visit(It.IsAny<Spacer>())).Verifiable();
            PrintVisitor.Setup(visitor => visitor.Visit(It.IsAny<Divider>())).Verifiable();
        }

        [TestCaseSource(nameof(parts))]
        public void Accept_WithPart_Runs(IPart part)
        {
            Assert.DoesNotThrow(() => part.Accept(PrintVisitor.Object));
        }
    }
}