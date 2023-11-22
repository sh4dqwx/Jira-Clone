using JiraClone.utils;
using JiraClone.utils.consoleViewParts;
using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using System;

namespace JiraClone.views
{
    public class WelcomeView: ConsoleView
    {
        private HorizontalMenu menu;

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
            VerticalMenu menu2 = new VerticalMenu(3);
			menu2.Add(new Button("Zaloguj się", loginView.Start));
			menu2.Add(new Button("Zarejestruj się", registerView.Start));
			menu2.Add(new Button("Zaloguj się", loginView.Start));
			menu2.Add(new Button("Zarejestruj się", registerView.Start));

			menu = new HorizontalMenu(2);
			menu.Add(new Button("Zaloguj się", loginView.Start));
            menu.Add(new Button("Zarejestruj się", registerView.Start));
			menu.Add(new Button("Zaloguj się", loginView.Start));
			menu.Add(new Button("Zarejestruj się", registerView.Start));
			menu.Add(new Button("Zaloguj się", loginView.Start));
			menu.Add(new Button("Zarejestruj się", registerView.Start));
			menu.Add(new Button("Zaloguj się", loginView.Start));
			menu.Add(new Button("Zarejestruj się", registerView.Start));

			Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(new Logo());
			Add(new Text("MENU GŁÓWNE"));
            Add(menu2);
		    Add(menu);
        }

        public void Start()
        {
            ResetView();

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    SelectNext();
                    continue;
                }
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    SelectPrevious();
                    continue;
                }
                UseKey(keyInfo.KeyChar);
                ResetView();
            }
        }
    }
}
