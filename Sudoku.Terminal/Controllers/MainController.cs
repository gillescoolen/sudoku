using Sudoku.Terminal.Views;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Terminal.Controllers
{
    public class MainController : Controller<MainController>
    {
        public MainController(App app) : base(app)
        {
            App.game.BaseSudoku = null;
        }

        public override View<MainController> CreateView()
        {
            return new MainView(this);
        }

        public void ChooseType()
        {
            App.game.BaseSudoku = App.parser.Parse(GetFormats().First(s => s.selected).type);
        }

        public override void Update()
        {
            if (App.game.BaseSudoku != null) App.OpenController<DefinitiveController>();
        }

        public List<(string type, bool selected)> GetFormats()
        {
            return App.game.GetFormats();
        }

        public void MoveSelection(int selection)
        {
            var selectedFormat = GetFormats().FindIndex(s => s.selected);
            var currentFormat = GetFormats()[selectedFormat];

            var nextIndex = selectedFormat == 0 && selection == -1 ? GetFormats().Count - 1 : selectedFormat + selection;
            var nextFormat = GetFormats().ElementAt(nextIndex >= GetFormats().Count ? 0 : nextIndex);

            var selectionList = GetFormats().Select(format =>
            {
                if (format == currentFormat)
                {
                    format.selected = false;
                }
                if (format == nextFormat)
                {
                    format.selected = true;
                }

                return format;
            }).ToList();

            App.game.SelectFormat(selectionList);

            App.OnNext(App.game);
        }
    }
}