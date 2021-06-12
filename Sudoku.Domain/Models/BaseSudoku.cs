using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Models
{
    public abstract class BaseSudoku
    {
        public List<IComponent> Sudokus { get; }

        protected BaseSudoku(List<IComponent> sudokus)
        {
            Sudokus = sudokus;
        }

        public virtual List<SquareLeaf> GetOrderedSquares()
        {
            return Sudokus.SelectMany(box => box.Find(leaf => !leaf.IsComposite()))
                .Cast<SquareLeaf>()
                .OrderBy(square => square.Position.Y)
                .ThenBy(square => square.Position.X)
                .Distinct(new DistinctLeafComparer())
                .ToList();
        }

        public int MaxValue()
        {
            return Sudokus.First()
                .GetChildren()
                .Max(box => box.GetChildren().Count());
        }

        public Board Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }

        private IEnumerable<IComponent> GetSudokus()
        {
            return Sudokus
                .Where(sudoku => sudoku.GetChildren().Count() > 2)
                .ToList();
        }

        public abstract IStrategy GetSolverStrategy();

        public virtual bool ValidateSudoku(State state, bool setValid = false)
        {
            GetOrderedSquares().ForEach(square => square.IsValid = true);
            return GetSudokus().All(sudoku => sudoku.Valid(state, setValid));
        }
    }
}