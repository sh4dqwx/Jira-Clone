using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.viewmodels
{
    public class WelcomeScreenViewModel
    {
        public void KeyPressed(ConsoleKeyInfo key)
        {
            Console.Write(key.ToString());
        }
    }
}
