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
        private CompoundPrintable layout;
        private Menu menu;
        private LoginView loginView = new();

        public WelcomeView(LoginView loginView)
        {
            Console.CursorVisible = false;

            menu = new Menu(Constants.MENU_WIDTH);
            menu.AddOption(new Option(menu.Width, "Zaloguj się", loginView.Start));
            menu.AddOption(new Option(menu.Width, "Zarejestruj się", () => { }));

            layout = new VerticalLayout(Constants.WINDOW_WIDTH);
            layout.Add(new Text(Constants.WINDOW_WIDTH, "Nacisnij CTRL+I aby zmienic interfejs"));
            layout.Add(new Logo(Constants.WINDOW_WIDTH));
			layout.Add(new Text(Constants.WINDOW_WIDTH, "MENU GŁÓWNE"));
			layout.Add(menu);
        }

        public void Start()
        {
            Console.Clear();
			layout.Print(0, 0);

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        menu.NavigateUp();
                        break;
                    case ConsoleKey.DownArrow:
                        menu.NavigateDown();
                        break;
                    case ConsoleKey.Enter:
                        menu.Enter();
                        Console.Clear();
                        layout.Print();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
