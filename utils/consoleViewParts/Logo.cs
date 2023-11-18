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

        public Logo(int height, int width) : base(height, width) { }

		public override void Print(int left, int top)
		{
            base.Print(left, top);
			for (int i=0; i<logo.Length; i++)
            {
                int marginLeft = (Width - logo[i].Length) / 2;
                Console.SetCursorPosition(left + marginLeft, top + i);
                Console.WriteLine(logo[i]);
			}
		}
	}
}
