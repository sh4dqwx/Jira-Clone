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
			HorizontalMenuBar hmb = new HorizontalMenuBar("IN PROGRESS", 1);

			VerticalMenu m1 = new VerticalMenu(2);
			m1.Add(new Button("Zaloguj się 1", () => { loginView.Start(); ResetView(); }));
			m1.Add(new Button("Zarejestruj się 1", () => { registerView.Start(); ResetView(); }));
			m1.Add(new Button("Zaloguj się 1", () => { loginView.Start(); ResetView(); }));
			m1.Add(new Button("Zarejestruj się 1", () => { registerView.Start(); ResetView(); }));

			VerticalMenu m2 = new VerticalMenu(2);

			VerticalMenu m3 = new VerticalMenu(2);
			m3.Add(new Button("Zaloguj się 3", () => { loginView.Start(); ResetView(); }));
			m3.Add(new Button("Zarejestruj się 3", () => { registerView.Start(); ResetView(); }));

			hmb.Add(m1);
			hmb.Add(m2);
			hmb.Add(m3);

			menu = new VerticalMenu(1);
			menu.Add(new Button("Zaloguj się", () => { loginView.Start(); ResetView(); }));
            menu.Add(new Button("Zarejestruj się", () => { registerView.Start(); ResetView(); }));

            HorizontalMenu hMenu = new HorizontalMenu(2);
			hMenu.Add(new Button("Zaloguj się", () => { loginView.Start(); ResetView(); }));
			hMenu.Add(new Button("Zarejestruj się", () => { registerView.Start(); ResetView(); }));

			Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(new Logo());
			Add(new Text("MENU GŁÓWNE"));
		    Add(hmb);
            Add(hMenu);
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
