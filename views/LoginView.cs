using JiraClone.utils.consoleViewParts;
using JiraClone.viewmodels;
using System;
using System.ComponentModel;
using JiraClone.utils;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.utils.consoleViewParts.layouts;
using System.Runtime.CompilerServices;

namespace JiraClone.views
{
    public class LoginView: IConsoleView
	{
        private CompoundPrintable layout;
        private Menu menu;
        private LoginViewModel viewModel;

		public LoginView()
		{
            //this.viewModel = viewModel;
            //viewModel.PropertyChanged += EventHandler;

            Console.CursorVisible = false;

            menu = new Menu(Constants.MENU_WIDTH);
            menu.AddOption(new Button(menu.Width, "Login", () => { }));
            menu.AddOption(new Button(menu.Width, "Hasło", () => { }));
			menu.AddOption(new Button(menu.Width, "Zatwierdź", () => { }));

			layout = new VerticalLayout(Constants.WINDOW_WIDTH);
            layout.Add(new Text(Constants.WINDOW_WIDTH, "Nacisnij CTRL+I aby zmienic interfejs"));
            layout.Add(new Logo(Constants.WINDOW_WIDTH));
			layout.Add(new Text(Constants.WINDOW_WIDTH, "LOGOWANIE"));
			layout.Add(menu);

        }

		private void EventHandler(object sender, PropertyChangedEventArgs e)
		{
			Console.WriteLine(e.PropertyName);
		}

		public void Start()
		{
            Console.Clear();
            layout.Print(0, 0);

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        menu.NavigateUp();
                        break;
                    case ConsoleKey.DownArrow:
                        menu.NavigateDown();
                        break;
                    case ConsoleKey.Enter:
                        menu.Enter();
                        break;
                    default:
                        break;
                }
            }
        }
	}
}
