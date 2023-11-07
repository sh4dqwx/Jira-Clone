using JiraClone.utils;
using JiraClone.utils.consoleViewParts;
using System;
using System.Linq;

namespace JiraClone.views
{
    public class WelcomeView
    {
        private CompoundPrintable layout;

        public WelcomeView()
        {
            layout = new VerticalLayout(Constants.WINDOW_WIDTH);
            layout.Add(new Text(Constants.WINDOW_WIDTH, "Nacisnij CTRL+I aby zmienic interfejs"));
            layout.Add(new Logo(Constants.WINDOW_WIDTH));
            layout.Add(new Option(Constants.MENU_WIDTH, "Zaloguj sie"));
        }

        public void Start()
        {
			layout.Print(0, 0);

			//while (true)
			//{
			//    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
			//    switch(keyInfo)
			//    {
			//        case ConsoleKey.UpArrow:
			//            moveSelectedUp();
			//            break;
			//        case ConsoleKey.DownArrow:
			//            moveSelectedDown();
			//            break;
			//        case ConsoleKey.Enter:
			//            enterSelected();
			//            break;
			//        default:
			//            break;
			//    }
			//}
		}

        private void enterSelected()
        {
            //switch (optionsCurrentSelectedId)
            //{
            //    case 0:
            //        // Przekierowanie do zalogowania
            //        return;
            //    case 1:
            //        //Przekierowanie do rejestracji
            //        return;
            //    default:
            //        return;
            //}
        }
    }
}
