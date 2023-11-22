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
    public class VerticalMenu : Layout
    {
        private int _visibleCount;
        public VerticalMenu(int visibleCount): base()
        {
            Height = 4;
            Width = Constants.MENU_WIDTH;
            _visibleCount = visibleCount;
        }

        public override void Print()
        {
			int cursorLeft = Left;
			int cursorTop = Top;

            int selectedIndex = selectedChild >= 0 ? selectedChild : 0;
            int startIndex = 0;
            //if (selectedIndex - _visibleCount / 2 >= 0 && selectedIndex + _visibleCount / 2 + _visibleCount % 2 < children.Count)
            //    startIndex = selectedIndex - _visibleCount / 2;
            //else if (selectedIndex - _visibleCount / 2 < 0)
            //    startIndex = 0;
            //else if (selectedIndex + _visibleCount / 2 >= children.Count)
            //    startIndex = children.Count - _visibleCount - 1;

            if (selectedIndex <= _visibleCount / 2) startIndex = 0;
            else if (selectedIndex >= children.Count - _visibleCount / 2)
                startIndex = children.Count - _visibleCount;
            else startIndex = selectedIndex - _visibleCount / 2;

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
        }

		public override void Add(Printable child)
		{
            if (child is not Option) throw new NotSupportedException();

            base.Add(child);
            if (children.Count > _visibleCount) return;
            if (children.Count > 1) Height++;
            Height += child.Height;
		}

		public override void Remove(Printable child)
		{
            if (child is not Option) throw new NotSupportedException();

            base.Remove(child);
            if (children.Count > _visibleCount) return;
            if (children.Count > 1) Height--;
            Height -= child.Height;
		}

        public override bool NavigateUp()
        {
            if (selectedChild <= 0)
                return false;

            UnselectSelected();
            ((Option)children[--selectedChild]).Selected = true;
            Print();

            return true;
        }

        public override bool NavigateDown()
        {
            if (selectedChild == -1 || selectedChild == children.Count - 1)
                return false;

            UnselectSelected();
            ((Option)children[++selectedChild]).Selected = true;
            Print();
            return true;
        }
    }
}
