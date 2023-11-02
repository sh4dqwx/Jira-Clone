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
        WelcomeScreenViewModel viewModel;

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
            ConsoleContentBuilder consoleContent = new ConsoleContentBuilder.Builder()
                .IncludeLogo()
                .Build();

            Console.WriteLine(consoleContent);

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                viewModel.KeyPressed(key);
            }
        }
    }
}
