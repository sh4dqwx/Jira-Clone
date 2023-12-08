using JiraClone.utils.consoleViewParts;
using JiraClone.viewmodels;
using System;
using System.ComponentModel;
using JiraClone.utils;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.utils.consoleViewParts.layouts;
using System.Runtime.CompilerServices;
using JiraClone.utils.validators;
using JiraClone.views.ProjectViews;

namespace JiraClone.views
{
    public class LoginView : ConsoleView
	{
		private LoginViewModel viewModel;
		private ProjectsView projectView;

        private VerticalMenu loginForm;
		private HorizontalMenu actionMenu;
		private Input loginInput, passwordInput;
		private Button submitButton;
		private Logo logo;
        private bool closeFlag = false;

		protected override void ResetView()
		{
			Clear();
			base.ResetView();
		}

		public LoginView(LoginViewModel viewModel, ProjectsView projectView)
		{
            this.viewModel = viewModel;
			this.projectView = projectView;
            viewModel.PropertyChanged += EventHandler;

			loginForm = new VerticalMenu("LOGOWANIE", 2);
			actionMenu = new HorizontalMenu(2);

			loginInput = new Input("Login", validationRule: new RequiredRule());
			passwordInput = new Input("Hasło", isPassword: true, validationRule: new RequiredRule());
			submitButton = new Button("Zatwierdź", OnSubmit);

            loginForm.Add(loginInput);
            loginForm.Add(passwordInput);

			actionMenu.Add(submitButton);
			actionMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

			logo = new Logo();

            Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(logo);
			Add(loginForm);
			Add(actionMenu);
        }

		private void EventHandler(object sender, PropertyChangedEventArgs e)
		{
			Console.WriteLine(e.PropertyName);
		}

        public void Start()
        {
            ResetView();
			Print();
			StartLoop(logo.ShiftToSide);

			while (true)
            {
                if (!Console.KeyAvailable)
                    continue;

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
				UseKey(keyInfo);

				if (closeFlag)
                {
                    closeFlag = false;
					EndLoop();
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
				Print();
			}
			else StartNewConsoleView(projectView.Start);
		}
	}
}
