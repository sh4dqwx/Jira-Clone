using JiraClone.utils;
using JiraClone.utils.consoleViewParts;
using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using System;

namespace JiraClone.views
{
    public class WelcomeView: ConsoleView
    {
        private VerticalMenu menu;
		private Logo logo;

        public WelcomeView(LoginView loginView, RegisterView registerView)
        {
			menu = new VerticalMenu("MENU GŁÓWNE", 2);
			menu.Add(new Button("Zaloguj się", () => { StartNewConsoleView(loginView.Start); }));
            menu.Add(new Button("Zarejestruj się", () => { StartNewConsoleView(registerView.Start); }));

			logo = new Logo();

			Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(logo);
			Add(menu);
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
			}
		}
	}
}
