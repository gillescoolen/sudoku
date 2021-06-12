#nullable enable

using System.Linq;
using System.Collections.Generic;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.States;
using Sudoku.Domain.Utilities;
using Sudoku.Domain.Models.Sudokus;

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

        public BaseSudoku? sudoku
        {
            get => context.Sudoku();
            set
            {
                context.SetSudoku(value);
                Board = context.CreateBoard();
                Notify(this);
            }
        }

        public Game(IContext context)
        {
            this.context = context;
            context.SwitchState(new DefinitiveState());
            Board = context.CreateBoard();
        }

        public IContext GetContext()
        {
            return context;
        }

        public void Solve()
        {
            if (sudoku == null) return;
            sudoku.GetOrderedSquares().ForEach(c => c.Value = "0");
            context.GetStrategy()?.Solve(sudoku, context.GetState()!);
            Notify(this);
        }

        public void ValidateSudoku(bool update = true)
        {
            if (sudoku == null) return;
            sudoku.ValidateSudoku(context.GetState()!, true);
            if (update) Notify(this);
        }

        public void SwitchState(State state)
        {
            context.SwitchState(state);
            Board = context.CreateBoard();
        }

        public void SelectSquare(Coordinate coordinate)
        {
            context.GetState()!.Select(coordinate);
            Notify(this);
        }

        public void EnterValue(string value)
        {
            if (int.Parse(value) > sudoku?.MaxValue()) return;

            var orderedSquares = sudoku?.GetOrderedSquares();

            var currentSquare = orderedSquares?.FirstOrDefault(square => square.IsSelected);

            if (currentSquare == null) return;

            context.EnterValue(value, currentSquare);

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