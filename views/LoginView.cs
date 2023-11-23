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
    public class LoginView : ConsoleView
	{
		private LoginViewModel viewModel;
		private ProjectsView projectView;

        private VerticalMenu menu;
		private Input loginInput, passwordInput;
		private Button submitButton;
        private bool closeFlag = false;

		private void ResetView()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.CursorVisible = false;

			loginInput.Clear();
			passwordInput.Clear();

			Print();
			SelectTop();
		}

		public LoginView(LoginViewModel viewModel, ProjectsView projectView)
		{
            this.viewModel = viewModel;
			this.projectView = projectView;
            viewModel.PropertyChanged += EventHandler;

			menu = new VerticalMenu(3);

			loginInput = new Input("Login", validationRule: new RequiredRule());
			passwordInput = new Input("Hasło", isPassword: true, validationRule: new RequiredRule());
			submitButton = new Button("Zatwierdź", OnSubmit);

            menu.Add(loginInput);
            menu.Add(passwordInput);
			menu.Add(submitButton);
			menu.Add(new Button("Powrót", () => { closeFlag = true; }));

            Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(new Logo());
			Add(new Text("LOGOWANIE"));
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
			}
			else
			{
				projectView.Start();
				ResetView();
			}
		}
	}
}
