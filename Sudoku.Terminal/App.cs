using System;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Terminal.Controllers;
using Sudoku.Terminal.Views;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Parser;

namespace Sudoku.Terminal
{
    public class App : IObserver<IGame>
    {
        public IGame game;
        public readonly ISudokuParser reader;
        private IDisposable unSubscriber;
        private Controller controller;
        private View view;
        private bool running = true;

        public App(IGame game, ISudokuParser reader)
        {
            this.game = game;
            this.reader = reader;
        }

        public async Task Run(string[] args)
        {
            Console.Title = "Sudoku";
            Console.OutputEncoding = Encoding.UTF8;

            OpenController<MainController>();
            unSubscriber = game.Subscribe(this);

            Input();

            await Task.CompletedTask;
        }

        public void Exit()
        {
            OnCompleted();
            running = false;
            Environment.Exit(1);
        }

        public void OpenController<T>() where T : Controller<T>
        {
            var newController = (T)Activator.CreateInstance(typeof(T), this);

            controller = newController;
            view = newController?.CreateView();
            OnNext(game);
        }

        public void OnCompleted()
        {
            unSubscriber.Dispose();
        }

        public void OnError(Exception error)
        {
            Console.Write($"Exception!: {error.Message}");
        }

        private void Input()
        {
            while (running)
            {
                var key = Console.ReadKey(true);

                foreach (var input in view.Inputs)
                {
                    if (input.Character != (int)key.Key)
                    {
                        continue;
                    }

                    input.Action();
                }
            }
        }

        public void OnNext(IGame value)
        {
            game = value;

            Console.Clear();

            controller.Update();
            var output = new StringBuilder();
            view.Draw(output);

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Write(output.ToString());
        }
    }
}