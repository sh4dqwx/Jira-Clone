using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.layouts
{
	public class VerticalLayout : Layout
	{
		public VerticalLayout(): base()
		{
			Width = Console.WindowWidth;
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

		public override void Add(Printable child)
		{
			Height += child.Height;
			if (children.Count > 1) Height++;
			base.Add(child);
		}
	}
}
