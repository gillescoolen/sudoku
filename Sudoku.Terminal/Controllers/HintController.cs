using System.Linq;
using System.Text;
using Sudoku.Terminal.Views;
using Sudoku.Terminal.Views.Visitors;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.States;

namespace Sudoku.Terminal.Controllers
{
    public class HintController : GameController<HintController>
    {
        public HintController(App app) : base(app)
        {
            App.game.BaseSudoku?.GetOrderedSquares().First().ToggleSelect();
        }

        public override View<HintController> CreateView()
        {
            App.game.TransitionState(new HintState());
            return new GameView<HintController>(this);
        }

        public override void Update()
        {

        }

        public override IDrawVisitor Visitor(StringBuilder builder)
        {
            return new DrawHintVisitor(builder);
        }

        public override void Switch()
        {
            App.OpenController<DefinitiveController>();
        }
    }
}