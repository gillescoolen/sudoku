using System.Linq;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.States;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain
{
    public class Game : Provider<Game>, IGame
    {
        private readonly IContext context;
        public Board Board { get; private set; }

        public SudokuWrapper? SudokuWrapper
        {
            get => context.SudokuWrapper();
            set
            {
                context.SetSudokuWrapper(value);
                Board = context.Construct();
                Notify(this);
            }
        }

        public Game(IContext context)
        {
            this.context = context;
            context.TransitionTo(new DefinitiveState());
            Board = context.Construct();
        }

        public IContext GetContext()
        {
            return context;
        }

        public void Solve()
        {
            if (SudokuWrapper == null) return;
            SudokuWrapper.GetOrderedCells().ForEach(c => c.Value = "0");
            context.GetStrategy()?.Solve(SudokuWrapper, context.GetState()!);
            Notify(this);
        }

        public void ValidateSudoku(bool update = true)
        {
            if (SudokuWrapper == null) return;
            SudokuWrapper.ValidateSudoku(context.GetState()!, true);
            if (update) Notify(this);
        }

        public void TransitionState(State state)
        {
            context.TransitionTo(state);
            Board = context.Construct();
        }

        public void SelectCell(Position position)
        {
            context.GetState()!.Select(position);
            Notify(this);
        }

        public void EnterValue(string value)
        {
            if (int.Parse(value) > SudokuWrapper?.MaxValue()) return;

            var orderedCells = SudokuWrapper?.GetOrderedCells();
            var currentLeaf = orderedCells?.FirstOrDefault(cellLeaf => cellLeaf.IsSelected);

            if (currentLeaf == null) return;

            context.EnterValue(value, currentLeaf);

            Notify(this);
        }
    }
}