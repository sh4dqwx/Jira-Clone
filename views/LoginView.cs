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

            menu = new Menu(0, Constants.MENU_WIDTH);
            menu.AddOption(new Input(5, menu.Width, "Login"));
            menu.AddOption(new Input(5, menu.Width, "Hasło", true));
			menu.AddOption(new Button(5, menu.Width, "Zatwierdź", () => { }));

			layout = new VerticalLayout(0, Constants.WINDOW_WIDTH);
            layout.Add(new Text(1, Constants.WINDOW_WIDTH, "Nacisnij CTRL+I aby zmienic interfejs"));
            layout.Add(new Logo(7, Constants.WINDOW_WIDTH));
			layout.Add(new Text(1, Constants.WINDOW_WIDTH, "LOGOWANIE"));
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
				if (keyInfo.Key == ConsoleKey.UpArrow)
				{
					menu.NavigateUp();
					continue;
				}
				if (keyInfo.Key == ConsoleKey.DownArrow)
				{
					menu.NavigateDown();
					continue;
				}
				menu.UseKey(keyInfo.KeyChar);
			}
        }
	}
}
