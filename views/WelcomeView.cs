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

        private void ResetView()
        {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.CursorVisible = false;

            Print();
            SelectTop();
		}

        public WelcomeView(LoginView loginView, RegisterView registerView)
        {
            menu = new VerticalMenu(1);
			menu.Add(new Button("Zaloguj się", loginView.Start));
            menu.Add(new Button("Zarejestruj się", registerView.Start));
			menu.Add(new Button("Zaloguj się", loginView.Start));
			menu.Add(new Button("Zarejestruj się", registerView.Start));

			VerticalMenu menu2 = new VerticalMenu(1);
			menu2.Add(new Button("Zaloguj się", loginView.Start));
			menu2.Add(new Button("Zarejestruj się", registerView.Start));

			Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(new Logo());
			Add(new Text("MENU GŁÓWNE"));
		    Add(menu);
            Add(menu2);
        }

        public void Start()
        {
            ResetView();

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    SelectUp();
                    continue;
                }
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    SelectDown();
                    continue;
                }
                UseKey(keyInfo.KeyChar);
                ResetView();
            }
        }
    }
}
