using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts
{
    public class Logo : Drawable
    {
        private string[] ConsoleLogo { get; } = new string[]
        {
            @"                                                     ",
            @" _____                            ___         __     ",
            @"/\___ \  __                     /'___`\     /'__`\   ",
            @"\/__/\ \/\_\  _ __    __       /\_\ /\ \   /\ \/\ \  ",
            @"   _\ \ \/\ \/\`'__\/'__`\     \/_/// /__  \ \ \ \ \ ",
            @"  /\ \_\ \ \ \ \ \//\ \L\.\_      // /_\ \__\ \ \_\ \",
            @"  \ \____/\ \_\ \_\\ \__/.\_\    /\______/\_\\ \____/",
            @"   \/___/  \/_/\/_/ \/__/\/_/    \/_____/\/_/ \/___/ ",
            @"                                                     ",
            @"                                                     "
        };

        public void Print()
        {
			foreach (var line in ConsoleLogo)
				PrintCenter(line);
		}

        public int Height
        {
            get { return ConsoleLogo.Length; }
        }

        public int Width
        {
            get { return ConsoleLogo[0].Length; }
        }
    }
}
