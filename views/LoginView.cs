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
    public class LoginView : IConsoleView
	{
        private CompoundPrintable layout;
        private Menu menu;
        private LoginViewModel viewModel;
		private Input loginInput, passwordInput;
        private bool closeFlag = false;

		public LoginView(LoginViewModel viewModel)
		{
            this.viewModel = viewModel;
            viewModel.PropertyChanged += EventHandler;

            Console.CursorVisible = false;

			menu = new Menu(0, Constants.MENU_WIDTH);

			loginInput = new Input(5, menu.Width, "Login");
			passwordInput = new Input(5, menu.Width, "Hasło", true);
            
			menu.Add(loginInput);
            menu.Add(passwordInput);
			menu.Add(new Button(5, menu.Width, "Zatwierdź", OnSubmit));
			menu.Add(new Button(5, menu.Width, "Powrót", () => { closeFlag = true; }));

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
			menu.NavigateTop();

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

				if(closeFlag)
				{
					closeFlag = false;
					menu.NavigateTop();
					return;
				}
			}
        }

		private void OnSubmit()
		{
			string? error = viewModel.AuthenticateUser(
				login: loginInput.Value,
				password: passwordInput.Value);

			if (error == null)
			{
				//Wyświetlić błąd
				return;
			}

		}
	}
}
