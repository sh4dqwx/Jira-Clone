using JiraClone.utils;
using JiraClone.utils.consoleViewParts;
using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.utils.validators;
using JiraClone.viewmodels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.views
{
    public class RegisterView : ConsoleView
	{
		private VerticalMenu menu;
		private RegisterViewModel viewModel;
		private Input loginInput, passwordInput, emailInput, nameInput, surnameInput;
		private Button submitButton;
		private bool closeFlag = false;

		private void ResetView()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.CursorVisible = false;

			loginInput.Clear();
			passwordInput.Clear();
			emailInput.Clear();
			nameInput.Clear();
			surnameInput.Clear();

			Print();
			SelectTop();
		}

		public RegisterView(RegisterViewModel viewModel)
		{
			this.viewModel = viewModel;
            viewModel.PropertyChanged += EventHandler;

            menu = new VerticalMenu(3);

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

			Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
			Add(new Logo());
			Add(new Text("REJESTRACJA"));
			Add(menu);
		}

        private void EventHandler(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(e.PropertyName);
        }

        public void Start()
		{
			ResetView();

			while (true)
			{
				ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectableChildren[selectedChild] is VerticalMenu)
                            SelectPrevious();
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectableChildren[selectedChild] is VerticalMenu)
                            SelectNext();
                        break;

                    case ConsoleKey.LeftArrow:
                        if (selectableChildren[selectedChild] is HorizontalMenu)
                            SelectPrevious();
                        break;

                    case ConsoleKey.RightArrow:
                        if (selectableChildren[selectedChild] is HorizontalMenu)
                            SelectNext();
                        break;

                    default:
                        UseKey(keyInfo.KeyChar);
                        break;
                }

				if (closeFlag)
                {
                    closeFlag = false;
                    ResetView();
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
