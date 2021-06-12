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

        public virtual List<CellLeaf> GetOrderedCells()
        {
            return Sudokus.SelectMany(box => box.Find(leaf => !leaf.IsComposite()))
                .Cast<CellLeaf>()
                .OrderBy(c => c.Position.Y)
                .ThenBy(c => c.Position.X)
                .Distinct(new DistinctLeafComparer())
                .ToList();
        }

        public int MaxValue()
        {
            return Sudokus.First()
                .GetChildren()
                .Max(q => q.GetChildren().Count());
        }

        public Board Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }

        private IEnumerable<IComponent> GetSudokus()
        {
            return Sudokus.Where(s => s.GetChildren().Count() > 2).ToList();
        }

        public abstract IStrategy GetSolverStrategy();

        public virtual bool ValidateSudoku(State state, bool setValid = false)
        {
            GetOrderedCells().ForEach(c => c.IsValid = true);
            return GetSudokus().All(sudoku => sudoku.Valid(state, setValid));
        }
    }
}