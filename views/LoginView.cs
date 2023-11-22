using JiraClone.utils.consoleViewParts;
using JiraClone.viewmodels;
using System;
using System.ComponentModel;
using JiraClone.utils;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.utils.consoleViewParts.layouts;
using System.Runtime.CompilerServices;
using JiraClone.utils.validators;

namespace JiraClone.views
{
    public class LoginView : IConsoleView
	{
		private LoginViewModel viewModel;

		private ConsoleView layout;
        private VerticalMenu menu;
		private Input loginInput, passwordInput;
		private Button submitButton;
        private bool closeFlag = false;

		public LoginView(LoginViewModel viewModel)
		{
            this.viewModel = viewModel;
            viewModel.PropertyChanged += EventHandler;

            Console.CursorVisible = false;

			menu = new VerticalMenu(3);

			loginInput = new Input("Login", validationRule: new RequiredRule());
			passwordInput = new Input("Hasło", isPassword: true, validationRule: new RequiredRule());
			submitButton = new Button("Zatwierdź", OnSubmit);


            menu.Add(loginInput);
            menu.Add(passwordInput);
			menu.Add(submitButton);
			menu.Add(new Button("Powrót", () => { closeFlag = true; }));

			layout = new ConsoleView();
			layout.SetBounds(0, 0, Console.WindowHeight, Console.WindowWidth);
            layout.Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            layout.Add(new Logo());
			layout.Add(new Text("LOGOWANIE"));
			layout.Add(menu);
        }

		private void EventHandler(object sender, PropertyChangedEventArgs e)
		{
			Console.WriteLine(e.PropertyName);
		}

		public void Start()
		{
            Console.Clear();

			loginInput.Clear();
			passwordInput.Clear();

            layout.Print();
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

		private bool AreInputsValid()
		{
			bool areValid = true;
			if (!loginInput.Validate()) areValid = false;
			if (!passwordInput.Validate()) areValid = false;
			return areValid;
		}

		private void OnSubmit()
		{
			if (!AreInputsValid()) return;

			string? error = viewModel.AuthenticateUser(
				login: loginInput.Value,
				password: passwordInput.Value);

			if (error != null)
			{
				submitButton.Error = error;
				submitButton.Print();
				return;
			}

		}
	}
}
