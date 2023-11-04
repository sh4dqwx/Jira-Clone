using JiraClone.utils.consoleViewParts;
using System;
using System.Linq;

namespace JiraClone.views
{
    public class WelcomeView
    {
        private IOption[] options = new IOption[]
        {
            new Option("Zaloguj się", 0, 14),
            new Option("Zarejestruj się", 0, 15)
        };

        public void Start()
        {
            DrawLayout();

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch(keyInfo)
                {
                    case ConsoleKey.UpArrow:
                        moveSelectedUp();
                        break;
                    case ConsoleKey.DownArrow:
                        moveSelectedDown();
                        break;
                    case ConsoleKey.Enter:
                        enterSelected();
                        break;
                    default:
                        break;
                }
            }
        }

        private void DrawLayout()
        {
            new Logo().Print();
            new Menu(options).Print();
        }

        private void enterSelected()
        {
            switch (optionsCurrentSelectedId)
            {
                case 0:
                    // Przekierowanie do zalogowania
                    return;
                case 1:
                    //Przekierowanie do rejestracji
                    return;
                default:
                    return;
            }
        }
    }
}
