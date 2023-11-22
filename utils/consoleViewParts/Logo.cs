using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts
{
    public class Logo : Printable
    {
        private readonly string[] logo = new string[]
        {
            @" _____                            ___         __     ",
            @"/\___ \  __                     /'___`\     /'__`\   ",
            @"\/__/\ \/\_\  _ __    __       /\_\ /\ \   /\ \/\ \  ",
            @"   _\ \ \/\ \/\`'__\/'__`\     \/_/// /__  \ \ \ \ \ ",
            @"  /\ \_\ \ \ \ \ \//\ \L\.\_      // /_\ \__\ \ \_\ \",
            @"  \ \____/\ \_\ \_\\ \__/.\_\    /\______/\_\\ \____/",
            @"   \/___/  \/_/\/_/ \/__/\/_/    \/_____/\/_/ \/___/ ",
        };

        public Logo() : base()
        {
            Height = logo.Length;
            Width = logo[0].Length;
        }

		public override void Print()
		{
			for (int i=0; i<logo.Length; i++)
            {
                int marginLeft = (Width - logo[i].Length) / 2;
                Console.SetCursorPosition(Left + marginLeft, Top + i);
                Console.WriteLine(logo[i]);
			}
		}
	}
}
