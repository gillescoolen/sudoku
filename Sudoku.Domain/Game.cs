#nullable enable

using System.Linq;
using System.Collections.Generic;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.States;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain
{
    public class Game : Provider<Game>, IGame
    {
        private readonly IContext context;

        private List<(string type, bool selected)> formats = new()
        {
            ("4x4", true),
            ("6x6", false),
            ("9x9", false),
            ("samurai", false),
            ("jigsaw", false)
        };

        public Board Board { get; private set; }

        public BaseSudoku? BaseSudoku
        {
            get => context.BaseSudoku();
            set
            {
                context.SetBaseSudoku(value);
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
            if (BaseSudoku == null) return;
            BaseSudoku.GetOrderedCells().ForEach(c => c.Value = "0");
            context.GetStrategy()?.Solve(BaseSudoku, context.GetState()!);
            Notify(this);
        }

        public void ValidateSudoku(bool update = true)
        {
            if (BaseSudoku == null) return;
            BaseSudoku.ValidateSudoku(context.GetState()!, true);
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
            if (int.Parse(value) > BaseSudoku?.MaxValue()) return;
            var orderedCells = BaseSudoku?.GetOrderedCells();
            var currentLeaf = orderedCells?.FirstOrDefault(cellLeaf => cellLeaf.IsSelected);
            if (currentLeaf == null) return;
            context.EnterValue(value, currentLeaf);
            Notify(this);
        }

        public List<(string type, bool selected)> GetFormats()
        {
            return formats;
        }

        public void SelectFormat(List<(string type, bool selected)> selection)
        {
            formats = selection;
        }
    }
}