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
			menu.Add(new Button("Zaloguj się", loginView.Start));
            menu.Add(new Button("Zarejestruj się", registerView.Start));

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

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (selectableChildren[selectedChild] is VerticalMenu)
                        SelectPrevious();
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (selectableChildren[selectedChild] is VerticalMenu)
                        SelectNext();
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
					if (selectableChildren[selectedChild] is HorizontalMenu)
                        SelectPrevious();
				}
				else if (keyInfo.Key == ConsoleKey.RightArrow)
				{
					if (selectableChildren[selectedChild] is HorizontalMenu)
                        SelectNext();
				}
                else
                {
					UseKey(keyInfo.KeyChar);
					ResetView();
				}
            }
        }
    }
}
