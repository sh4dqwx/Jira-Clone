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
			menu = new VerticalMenu(2);
			menu.Add(new Button("Zaloguj się", () => { loginView.Start(); ResetView(); }));
            menu.Add(new Button("Zarejestruj się", () => { registerView.Start(); ResetView(); }));

			Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(new Logo());
			Add(new Text("MENU GŁÓWNE"));
		    Add(menu);
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
                        else if (selectableChildren[selectedChild] is HorizontalMenu)
                        {
                            selectableChildren[selectedChild].SelectTop();
                            SelectPrevious();
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectableChildren[selectedChild] is VerticalMenu)
                            SelectNext();
                        else if (selectableChildren[selectedChild] is HorizontalMenu)
                        {
                            selectableChildren[selectedChild].SelectBottom();
                            SelectNext();
                        }
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
            }
        }
    }
}
