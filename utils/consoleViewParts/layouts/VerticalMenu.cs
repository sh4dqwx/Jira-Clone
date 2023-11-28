using JiraClone.utils.consoleViewParts.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.layouts
{
    public class VerticalMenu : Menu
    {
        private string? _title;

        public VerticalMenu(string? title, int visibleCount): base()
        {
			Height = (visibleCount * Constants.BUTTON_HEIGHT) + (visibleCount - 1);
            if (title != null) Height += 6;
            else Height += 4;
            Width = Constants.BUTTON_WIDTH;
            _title = title;
            _visibleCount = visibleCount;
        }

        public override void Print()
        {
			int cursorLeft = Left;
			int cursorTop = Top;

            int startIndex = GetStartIndex();

            if (_title != null)
            {
				Console.SetCursorPosition(cursorLeft + (Width - _title.Length) / 2, cursorTop);
				Console.Write(_title);
				cursorTop += 2;
			}

			Console.SetCursorPosition(cursorLeft + (Width - 1) / 2, cursorTop);
            if (startIndex > 0) Console.Write("^");
            else Console.Write(" ");
            cursorTop += 2;

            for(int i = startIndex; i < Math.Min(_visibleCount + startIndex, children.Count); i++)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop);
                Printable child = children[i];
				child.Left = cursorLeft; child.Top = cursorTop;
                child.Print();
                cursorTop += child.Height + 1;
            }
            cursorTop = Top + Height - 1;

            Console.SetCursorPosition(cursorLeft + ((Width - 1) / 2), cursorTop);
            if (startIndex < children.Count - _visibleCount) Console.Write("v");
            else Console.Write(" ");

			if (selectedChild != -1) children[selectedChild].Print();
		}

		public override bool UseKey(ConsoleKeyInfo c)
		{
			if (selectedChild < 0) return false;

            switch (c.Key)
            {
                case ConsoleKey.UpArrow:
                    bool upResult = SelectPrevious();
                    if (upResult) Print();
                    return upResult;

                case ConsoleKey.DownArrow:
				case ConsoleKey.Tab:
					bool downResult = SelectNext();
                    if (downResult) Print();
                    return downResult;

                default:
					return base.UseKey(c);
			}
		}

		public override void ClearChildren()
        {
            base.ClearChildren();
            selectedChild = -1;
        }
    }
}
