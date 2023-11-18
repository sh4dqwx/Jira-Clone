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

		public Button(int height, int width, string name, Action callback) : base(height, width, name)
		{
			_callback = callback;
		}

		public override void Print(int left, int top)
		{
			base.Print(left, top);
			int marginLeft = (Width - Name.Length) / 2;
			Console.SetCursorPosition(Left + marginLeft, Top);
			if(Selected)
				Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(Name);

            if (Error.Length > 0)
            {
				int errorMarginLeft = (Width - Error.Length) / 2;
                Console.SetCursorPosition(Left + errorMarginLeft, Top + 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("↑ " + Error + " ↑");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

		public override void UseKey(char c)
		{
			if(c == '\n' || c == '\r') _callback();
		}
	}
}
