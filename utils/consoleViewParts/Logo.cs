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

        public Logo(int width) : base(width) { }

		public override void Print(int left, int top)
		{
			for (int i=0; i<logo.Length; i++)
            {
				Console.SetCursorPosition((width - logo[i].Length) / 2 + left, top + i);
                Console.WriteLine(logo[i]);
			}
		}
	}
}
