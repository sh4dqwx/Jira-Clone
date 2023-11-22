using JiraClone.utils;
using JiraClone.utils.consoleViewParts;
using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.utils.validators;
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
		private VerticalLayout layout;
		private VerticalMenu menu;
		private RegisterViewModel viewModel;
		private Input loginInput, passwordInput, emailInput, nameInput, surnameInput;
		private Button submitButton;
		private bool closeFlag = false;

		public RegisterView(RegisterViewModel viewModel)
		{
			this.viewModel = viewModel;
			Console.CursorVisible = false;

			menu = new VerticalMenu(3, 1);
			menu.Height = 50; menu.Width = Console.WindowWidth;

			loginInput = new Input("Login", validationRule: new RequiredRule());
			passwordInput = new Input("Hasło", true, validationRule: new RequiredRule());
			emailInput = new Input("Email", validationRule: new EmailRule());
			nameInput = new Input("Imię", validationRule: new RequiredRule());
			surnameInput = new Input("Nazwisko", validationRule: new RequiredRule());
			submitButton = new Button("Zatwierdź", OnSubmit);


            menu.Add(loginInput);
			menu.Add(passwordInput);
			menu.Add(emailInput);
			menu.Add(nameInput);
			menu.Add(surnameInput);
			menu.Add(submitButton);
			menu.Add(new Button("Powrót", () => { closeFlag = true; }));

			layout = new VerticalLayout();
			layout.SetBounds(0, 0, Console.WindowHeight, Console.WindowWidth);
			layout.Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
			layout.Add(new Logo());
			layout.Add(new Text("REJESTRACJA"));
			layout.Add(menu);
		}

		public void Start()
		{
			Console.Clear();

			loginInput.Clear();
			passwordInput.Clear();
			emailInput.Clear();
			nameInput.Clear();
			surnameInput.Clear();

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

				if (closeFlag)
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
			if (!emailInput.Validate()) areValid = false;
			if (!nameInput.Validate()) areValid = false;
			if (!surnameInput.Validate()) areValid = false;
			return areValid;
		}

		private void OnSubmit()
		{
			if (!AreInputsValid()) return;

			string? error = viewModel.RegisterUser(
				login: loginInput.Value,
				password: passwordInput.Value,
				email: emailInput.Value,
				name: nameInput.Value,
				surname: surnameInput.Value);

            if (error != null)
            {
                submitButton.Error = error;
                submitButton.Print();
                return;
            }
        }
	}
}
