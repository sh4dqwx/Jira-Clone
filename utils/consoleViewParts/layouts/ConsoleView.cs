using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.layouts
{
	public class ConsoleView : Layout
	{
		public ConsoleView(): base()
		{
			Height = Constants.WINDOW_HEIGHT;
			Width = Constants.WINDOW_WIDTH;
		}

		public override void Print()
		{
			int cursorLeft = Left;
			int cursorTop = Top;

			foreach (Printable child in children)
			{
				Console.SetCursorPosition(cursorLeft, cursorTop);
				child.Left = (Width - child.Width) / 2 + Console.CursorLeft;
				child.Top = Console.CursorTop;
				child.Print();
				
				cursorTop += child.Height + 1;
			}
		}
	}
}
