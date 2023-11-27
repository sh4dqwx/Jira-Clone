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
			menu.Add(new Button("Zaloguj sięZaloguj sięZaloguj sięZaloguj sięZaloguj sięZaloguj sięZaloguj sięZaloguj sięZaloguj się", () => { loginView.Start(); ResetView(); Print(); }));
            menu.Add(new Button("Zarejestruj się", () => { registerView.Start(); ResetView(); Print(); }));

			Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(new Logo());
			Add(menu);
        }

		public void Start()
		{
			ResetView();
			Print();

			while (true)
			{
				ConsoleKeyInfo keyInfo = Console.ReadKey(true);
				UseKey(keyInfo);
			}
		}
	}
}
