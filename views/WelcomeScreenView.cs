using JiraClone.utils;
using JiraClone.viewmodels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.views
{
    public class WelcomeScreenView
    {
        private WelcomeScreenViewModel viewModel;
        //To trzeba gdzieś schować
        private readonly string logo = "\r\n _____                            ___         __     \r\n/\\___ \\  __                     /'___`\\     /'__`\\   \r\n\\/__/\\ \\/\\_\\  _ __    __       /\\_\\ /\\ \\   /\\ \\/\\ \\  \r\n   _\\ \\ \\/\\ \\/\\`'__\\/'__`\\     \\/_/// /__  \\ \\ \\ \\ \\ \r\n  /\\ \\_\\ \\ \\ \\ \\ \\//\\ \\L\\.\\_      // /_\\ \\__\\ \\ \\_\\ \\\r\n  \\ \\____/\\ \\_\\ \\_\\\\ \\__/.\\_\\    /\\______/\\_\\\\ \\____/\r\n   \\/___/  \\/_/\\/_/ \\/__/\\/_/    \\/_____/\\/_/ \\/___/ \r\n                                                     \r\n                                                     \r\n";
        private readonly int optionsMarginLeft = 10; //10 spaces
        private readonly int optionsMarginTop = 13; //13 lines
        private readonly List<string> options = new() { "Zaloguj", "Zarejestruj" };
        private string marginLeftInString;
        private int optionsCurrentSelectedId = 0;

        public WelcomeScreenView()
        {
            viewModel = new();

            viewModel.PropertyChanged += EventHandler;

            StringBuilder marginLeft = new StringBuilder();
            for (int i = 0; i < optionsMarginLeft; i++)
                marginLeft.Append(" ");
            marginLeftInString = marginLeft.ToString();
        }

        private void EventHandler(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(e.PropertyName);
        }

        public void Start()
        {
            DrawLayout();

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.UpArrow)
                    moveSelectedUp();
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                    moveSelectedDown();
                else if (keyInfo.Key == ConsoleKey.Enter)
                    enterSelected();
            }
        }

        private void DrawLayout()
        {
            Console.WriteLine(logo);
            Console.Write("\n\n");

            for (int i = 0; i < options.Count; i++)
            {
                if (i == 0)
                    Console.WriteLine(marginLeftInString + "-> " + options[i]);
                else
                    Console.WriteLine(marginLeftInString + "*  " + options[i]);
            }
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

        private void moveSelectedUp()
        {
            if (optionsCurrentSelectedId <= 0) return;

            (int leftPosition, int topPosition) = Console.GetCursorPosition();

            Console.SetCursorPosition(optionsMarginLeft, optionsMarginTop + optionsCurrentSelectedId);
            Console.Write("* ");
            optionsCurrentSelectedId--;
            Console.SetCursorPosition(optionsMarginLeft, optionsMarginTop + optionsCurrentSelectedId);
            Console.Write("->");

            Console.SetCursorPosition(leftPosition - 1, topPosition);
        }

        private void moveSelectedDown()
        {
            if (optionsCurrentSelectedId >= options.Count - 1) return;

            (int leftPosition, int topPosition) = Console.GetCursorPosition();

            Console.SetCursorPosition(optionsMarginLeft, optionsMarginTop + optionsCurrentSelectedId);
            Console.Write("* ");
            optionsCurrentSelectedId++;
            Console.SetCursorPosition(optionsMarginLeft, optionsMarginTop + optionsCurrentSelectedId);
            Console.Write("->");

            Console.SetCursorPosition(leftPosition - 1, topPosition);
        }
    }
}
