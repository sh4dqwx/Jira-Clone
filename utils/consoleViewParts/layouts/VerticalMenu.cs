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
        public VerticalMenu(int visibleCount): base()
        {
            Height = 4;
            Width = Constants.BUTTON_WIDTH;
            _visibleCount = visibleCount;
        }

        public override void Print()
        {
			int cursorLeft = Left;
			int cursorTop = Top;

            int startIndex = GetStartIndex();

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

            Console.SetCursorPosition(cursorLeft + ((Width - 1) / 2), cursorTop);
            if (startIndex < children.Count - _visibleCount) Console.Write("v");
            else Console.Write(" ");

			if(selectedChild != -1) children[selectedChild].Print();
		}

		public override void Add(Printable child)
		{
            base.Add(child);
            if (children.Count > _visibleCount) return;
            if (children.Count > 1) Height++;
            Height += child.Height;
		}

		public override void Remove(Printable child)
		{
            base.Remove(child);
            if (children.Count > _visibleCount) return;
            if (children.Count > 1) Height--;
            Height -= child.Height;
		}

        public override void Clear()
        {
            base.Clear();
            Height = 4;
            selectedChild = -1;
        }
    }
}
