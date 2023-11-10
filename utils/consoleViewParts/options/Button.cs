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

			int nameLaneLeft = (_width - 2 - _name.Length) / 2 - 4;
			int nameLaneRight = nameLaneLeft + (_width - 2 - _name.Length) % 2;

			if (Selected)
				Console.ForegroundColor = ConsoleColor.Cyan;

			var bufferHeight = Console.BufferHeight;
			Console.SetCursorPosition(left, top + 2);
			Console.WriteLine(new StringBuilder()
				.Append('|')
				.Append(' ', nameLaneLeft)
				.Append(Selected ? "->  " : " *  ")
				.Append(_name)
				.Append(Selected ? "  <-" : "  * ")
				.Append(' ', nameLaneRight)
				.Append('|')
				.ToString()
			);
			Console.CursorTop += 2;

			Console.ForegroundColor = ConsoleColor.White;
		}

		public override void UseKey(char c)
		{
			if(c == '\n' || c == '\r') _callback();
		}
	}
}
