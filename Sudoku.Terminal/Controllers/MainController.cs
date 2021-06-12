using Sudoku.Terminal.Views;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Terminal.Controllers
{
    public class MainController : Controller<MainController>
    {
        public MainController(App app) : base(app)
        {
            App.game.SudokuWrapper = null;
        }

        public override View<MainController> CreateView()
        {
            return new MainView(this);
        }

        public void ChooseType()
        {
            App.game.SudokuWrapper = App.reader.Parse(GetFormats().First(s => s.selected).type);
        }

        public override void Update()
        {
            if (App.game.SudokuWrapper != null) App.OpenController<DefinitiveController>();
        }

        public List<(string type, bool selected)> GetFormats()
        {
            return App.game.GetFormats();
        }

        public void MoveSelection(int selection)
        {
            var selected = GetFormats().FindIndex(s => s.selected);
            var current = GetFormats()[selected];

            var nextIndex = selected == 0 && selection == -1 ? GetFormats().Count - 1 : selected + selection;
            var next = GetFormats().ElementAt(nextIndex >= GetFormats().Count ? 0 : nextIndex);

            var selectionList = GetFormats().Select(s =>
            {
                if (s == current) s.selected = false;
                if (s == next) s.selected = true;
                return s;
            }).ToList();
            
            App.game.SelectFormat(selectionList);

            App.OnNext(App.game);
        }
    }
}