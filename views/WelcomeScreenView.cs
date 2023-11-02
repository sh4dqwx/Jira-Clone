using JiraClone.utils;
using JiraClone.viewmodels;
using System;
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
        private static readonly string logo = "\r\n _____                            ___         __     \r\n/\\___ \\  __                     /'___`\\     /'__`\\   \r\n\\/__/\\ \\/\\_\\  _ __    __       /\\_\\ /\\ \\   /\\ \\/\\ \\  \r\n   _\\ \\ \\/\\ \\/\\`'__\\/'__`\\     \\/_/// /__  \\ \\ \\ \\ \\ \r\n  /\\ \\_\\ \\ \\ \\ \\ \\//\\ \\L\\.\\_      // /_\\ \\__\\ \\ \\_\\ \\\r\n  \\ \\____/\\ \\_\\ \\_\\\\ \\__/.\\_\\    /\\______/\\_\\\\ \\____/\r\n   \\/___/  \\/_/\\/_/ \\/__/\\/_/    \\/_____/\\/_/ \\/___/ \r\n                                                     \r\n                                                     \r\n";

        public WelcomeScreenView()
        {
            viewModel = new();

            viewModel.PropertyChanged += EventHandler;
        }

        private void EventHandler(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(e.PropertyName);
        }

        public void Start()
        {
            Console.WriteLine(logo);

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                viewModel.KeyPressed(key);
            }
        }
    }
}
