using JiraClone.utils.consoleViewParts.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.layouts
{
    public class HorizontalMenu : Menu
	{
		public HorizontalMenu(int visibleCount): base()
		{
			Height = Constants.BUTTON_HEIGHT;
			Width = 4;
			_visibleCount = visibleCount;
		}

		public override void Print()
		{
			int cursorLeft = Left;
			int cursorTop = Top;

			int startIndex = GetStartIndex();

			Console.SetCursorPosition(cursorLeft, cursorTop + (Height - 1) / 2);
			if (startIndex > 0) Console.Write("<");
			else Console.Write(" ");
			cursorLeft += 2;

			for (int i = startIndex; i < Math.Min(_visibleCount + startIndex, children.Count); i++)
			{
				Console.SetCursorPosition(cursorLeft, cursorTop);
				Printable child = children[i];
				child.Left = cursorLeft; child.Top = cursorTop;
				child.Print();
				cursorLeft += child.Width + 1;
			}

			Console.SetCursorPosition(cursorLeft, cursorTop + (Height - 1) / 2);
			if (startIndex < children.Count - _visibleCount) Console.Write(">");
			else Console.Write(" ");

			if(selectedChild != -1) children[selectedChild].Print();
		}

		public override void Add(Printable child)
		{
			if (child is not Option) throw new NotSupportedException();

			base.Add(child);
			if (children.Count > _visibleCount) return;
			if (children.Count > 1) Width++;
			Width += child.Width;
		}

		public override void Remove(Printable child)
		{
			if (child is not Option) throw new NotSupportedException();

			base.Remove(child);
			if (children.Count > _visibleCount) return;
			if (children.Count > 1) Width--;
			Width -= child.Width;
		}

		public override void Clear()
		{
			base.Clear();
			Width = 4;
			selectedChild = -1;
		}
	}
}
