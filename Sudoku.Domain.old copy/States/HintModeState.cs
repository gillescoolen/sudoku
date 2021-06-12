using Sudoku.Domain.Models;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.States
{
    public class HintState : State
    {
        public override void EnterValue(string value, CellLeaf cell)
        {
            throw new System.NotImplementedException();
        }

        public override Board Construct()
        {
            throw new System.NotImplementedException();
        }
    }
}