using System.Linq;
using System.Text;
using Sudoku.Terminal.Views;
using Sudoku.Terminal.Views.Visitors;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.States;

namespace Sudoku.Terminal.Controllers
{
    public class DefinitiveController : GameController<DefinitiveController>
    {
        public DefinitiveController(App app) : base(app)
        {
            App.game.BaseSudoku?.GetOrderedSquares().First().ToggleSelect();
        }

        public override View<DefinitiveController> CreateView()
        {
            App.game.TransitionState(new DefinitiveState());
            return new GameView<DefinitiveController>(this);
        }

        public override void Update()
        {

        }

        public override IDrawVisitor Visitor(StringBuilder builder)
        {
            return new DrawDefinitiveVisitor(builder);
        }

        public override void Switch()
        {
            App.OpenController<HintController>();
        }
    }
}