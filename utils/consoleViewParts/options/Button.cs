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
            if (Selected)
                Console.ForegroundColor = ConsoleColor.Cyan;

            Console.SetCursorPosition(cursorLeft, cursorTop);
			Console.Write(new StringBuilder()
				.Append('+')
				.Append('-', Width - 2)
				.Append('+'));
			cursorTop++;

            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(new StringBuilder()
                .Append('|')
                .Append(' ', Width - 2)
                .Append('|'));
            cursorTop++;

            int marginLeft = Math.Max(0, (Width - 2 - Name.Length) / 2);
			Console.SetCursorPosition(cursorLeft, cursorTop);
			Console.Write(new StringBuilder()
				.Append('|')
				.Append(' ', marginLeft)
				.Append(Name)
				.Append(' ', Width - 2 - Name.Length - marginLeft)
				.Append('|'));
			cursorTop++;

            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(new StringBuilder()
                .Append('|')
                .Append(' ', Width - 2)
                .Append('|'));
            if (Error.Length > 0)
            {
				int errorMarginLeft = (Width - Error.Length) / 2;
                Console.SetCursorPosition(cursorLeft + errorMarginLeft, cursorTop);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("↑ " + Error + " ↑");
				Console.ForegroundColor = ConsoleColor.Cyan;
            }
			cursorTop++;

            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.Write(new StringBuilder()
                .Append('+')
                .Append('-', Width - 2)
                .Append('+'));

            Console.ForegroundColor = ConsoleColor.White;
        }

		public override void UseKey(char c)
		{
			if(c == '\n' || c == '\r') _callback();
		}
	}
}
