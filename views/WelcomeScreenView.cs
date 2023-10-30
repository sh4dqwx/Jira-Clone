using JiraClone.utils;
using JiraClone.viewmodels;
using System;
using System.Collections.Generic;
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
