using JiraClone.utils;
using JiraClone.utils.consoleViewParts;
using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JiraClone.views
{
    public class WelcomeView: IConsoleView
    {
        private ConsoleView layout;
        private VerticalMenu menu;

        private void ResetView()
        {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.CursorVisible = false;

            layout.Print();
            menu.NavigateTop();
		}

        public WelcomeView(LoginView loginView, RegisterView registerView)
        {
            menu = new VerticalMenu(3);
			menu.Add(new Button("Zaloguj się", loginView.Start));
            menu.Add(new Button("Zarejestruj się", registerView.Start));

            layout = new ConsoleView();
			layout.Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            layout.Add(new Logo());
			layout.Add(new Text("MENU GŁÓWNE"));
			layout.Add(menu);
        }

        public void Start()
        {
            ResetView();

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
                ResetView();
            }
        }
    }
}
