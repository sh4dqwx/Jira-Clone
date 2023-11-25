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

        public WelcomeView(LoginView loginView, RegisterView registerView)
        {
			menu = new VerticalMenu("MENU GŁÓWNE", 2);
			menu.Add(new Button("Zaloguj się", () => { loginView.Start(); ResetView(); }));
            menu.Add(new Button("Zarejestruj się", () => { registerView.Start(); ResetView(); }));

			Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(new Logo());
			Add(menu);
        }

		public void Start()
		{
			ResetView();

			while (true)
			{
				ConsoleKeyInfo keyInfo = Console.ReadKey(true);
				UseKey(keyInfo);
			}
		}
	}
}
