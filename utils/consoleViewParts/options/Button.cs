using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.options
{
	public class Button : Option
	{
		public Button(int width, string name, Action callback) : base(width, name, callback)
		{
		}

		public override void Print(int left, int top)
		{
			base.Print(left, top);

			int emptyLane = width - 2;
			int nameLaneLeft = (emptyLane - _name.Length) / 2 - 4;
			int nameLaneRight = nameLaneLeft + (emptyLane - _name.Length) % 2;

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

			if (Selected)
				Console.ForegroundColor = ConsoleColor.White;
		}
	}
}
