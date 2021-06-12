#nullable enable

using Sudoku.Domain.Models;

namespace Sudoku.Domain.Utilities
{
    public abstract class State
    {
        protected Context? Context;

        public void SetContext(Context context)
        {
            Context = context;
        }

        public abstract void EnterValue(string value, CellLeaf cell);
        public abstract Board? Construct();
    }
}