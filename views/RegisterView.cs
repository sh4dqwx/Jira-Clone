using JiraClone.utils;
using JiraClone.utils.consoleViewParts;
using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JiraClone.views
{
	public class RegisterView : IConsoleView
	{
		private CompoundPrintable layout;
		private Menu menu;
		private RegisterViewModel viewModel;
		private Input loginInput, passwordInput, emailInput, nameInput, surnameInput;
		private bool closeFlag = false;

		public RegisterView(RegisterViewModel viewModel)
		{
			this.viewModel = viewModel;
			Console.CursorVisible = false;

			menu = new Menu(0, Constants.MENU_WIDTH);

			loginInput = new Input(5, menu.Width, "Login");
			passwordInput = new Input(5, menu.Width, "Hasło", true);
			emailInput = new Input(5, menu.Width, "Email");
			nameInput = new Input(5, menu.Width, "Imię");
			surnameInput = new Input(5, menu.Width, "Nazwisko");

			menu.Add(loginInput);
			menu.Add(passwordInput);
			menu.Add(emailInput);
			menu.Add(nameInput);
			menu.Add(surnameInput);
			menu.Add(new Button(5, menu.Width, "Zatwierdź", OnSubmit));
			menu.Add(new Button(5, menu.Width, "Powrót", () => { closeFlag = true; }));

			layout = new VerticalLayout(0, Constants.WINDOW_WIDTH);
			layout.Add(new Text(1, Constants.WINDOW_WIDTH, "Nacisnij CTRL+I aby zmienic interfejs"));
			layout.Add(new Logo(7, Constants.WINDOW_WIDTH));
			layout.Add(new Text(1, Constants.WINDOW_WIDTH, "REJESTRACJA"));
			layout.Add(menu);
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

				if (closeFlag)
				{
					closeFlag = false;
					menu.NavigateTop();
					return;
				}
			}
		}

		private void OnSubmit()
		{
			string? error = viewModel.RegisterUser(
				login: loginInput.Value,
				password: passwordInput.Value,
				email: emailInput.Value,
				name: nameInput.Value,
				surname: surnameInput.Value);

            if (error == null)
            {
                //Wyświetlić błąd
                return;
            }
        }
	}
}
