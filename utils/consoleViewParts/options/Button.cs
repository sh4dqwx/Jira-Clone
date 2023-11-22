using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.options
{
	public class Button : Option
	{
		private Action _callback;

		public Button(string name, Action callback) : base(name)
		{
			Height = 5;
			Width = Constants.MENU_WIDTH;
			_callback = callback;
		}

		public override void Print()
		{
			int cursorLeft = Left;
			int cursorTop = Top;
			if (Selected) Console.ForegroundColor = ConsoleColor.Cyan;
			else Console.ForegroundColor = ConsoleColor.White;
			Console.CursorVisible = false;

			base.Print();

			int marginLeft = Math.Max(0, (Width - Name.Length) / 2);
			Console.SetCursorPosition(cursorLeft + marginLeft, cursorTop + 2);
			Console.Write(Name);
			cursorTop++;

            if (Error.Length > 0)
            {
				int errorMarginLeft = (Width - Error.Length) / 2;
                Console.SetCursorPosition(cursorLeft + errorMarginLeft, cursorTop);

				ConsoleColor prevColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("↑ " + Error + " ↑");
				Console.ForegroundColor = prevColor;
            }
        }

		public override void UseKey(char c)
		{
			if(c == '\n' || c == '\r') _callback();
		}
	}
}
