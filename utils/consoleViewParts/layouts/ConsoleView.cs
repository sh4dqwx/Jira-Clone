using JiraClone.utils.consoleViewParts.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.layouts
{
    public class ConsoleView : Layout, ISelectable
	{
		protected List<ISelectable> selectableChildren;

		public ConsoleView(): base()
		{
			Height = Constants.WINDOW_HEIGHT;
			Width = Constants.WINDOW_WIDTH;
			selectableChildren = new();
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
			base.Add(child);
			if (child is ISelectable) selectableChildren.Add((ISelectable)child);
		}

		public override void Remove(Printable child)
		{
			base.Remove(child);
			if (child is ISelectable) selectableChildren.Remove((ISelectable)child);
		}

		public void UnselectSelected()
		{
			if (selectedChild == -1)
				return;

			if (selectableChildren[selectedChild] is Option)
			{
				selectableChildren[selectedChild].Selected = false;
				((Printable)selectableChildren[selectedChild]).Print();
			}
			else selectableChildren[selectedChild].UnselectSelected();

			selectableChildren[selectedChild].Selected = false;
		}

		public bool SelectTop()
		{
			if (selectableChildren.Count == 0)
				return false;

			UnselectSelected();

			selectedChild = 0;

			for (int i = 0; i < selectableChildren.Count; i++, selectedChild = i)
            {
                ISelectable child = selectableChildren[i];
                if (child is Option)
                {
                    child.Selected = true;
                    ((Printable)child).Print();
                    return true;
                }

                if (child.SelectTop())
                    return true;
            }

			return false;
        }

		public bool SelectBottom()
		{
			if (selectableChildren.Count == 0) 
				return false;

			UnselectSelected();

			selectedChild = selectableChildren.Count - 1;

            for (int i = selectableChildren.Count - 1; i >= 0; i--, selectedChild = i)
            {
				ISelectable child = selectableChildren[i];
                if (child is Option)
                {
                    child.Selected = true;
                    ((Printable)child).Print();
                    return true;
                }

                if (child.SelectBottom())
                    return true;
            }

			return false;
        }

		public bool SelectPrevious()
		{
			if (selectedChild == -1) return false;

			if (selectableChildren[selectedChild] is Option)
			{
				if (selectedChild <= 0) return false;
				else
				{
					UnselectSelected();
					selectableChildren[--selectedChild].Selected = true;
					((Printable)selectableChildren[selectedChild]).Print();
					return true;
				}
			}
			else
			{
				bool result = selectableChildren[selectedChild].SelectPrevious();
				if (result == true) return true;
				if (selectedChild <= 0)
				{
					selectableChildren[selectedChild].SelectTop();
					return false;
                }
				if (!selectableChildren[--selectedChild].SelectBottom())
				{
                    selectableChildren[++selectedChild].SelectTop();
					return false;
                }
				return true;
			}
		}

		public bool SelectNext()
		{
			if (selectedChild == -1) return false;

			if (selectableChildren[selectedChild] is Option)
			{
				if (selectedChild >= selectableChildren.Count - 1) return false;
				else
				{
					UnselectSelected();
					selectableChildren[++selectedChild].Selected = true;
					((Printable)selectableChildren[selectedChild]).Print();
					return true;
				}
			}
			else
			{
				bool result = selectableChildren[selectedChild].SelectNext();
				if (result == true) return true;
				if (selectedChild >= selectableChildren.Count - 1)
				{
					selectableChildren[selectedChild].SelectBottom();
					return false;
                }
				if (!selectableChildren[++selectedChild].SelectTop())
				{
					selectableChildren[--selectedChild].SelectBottom();
					return false;
				}
				return true;
			}
		}

		public void UseKey(char c)
		{
			if (selectedChild < 0) return;
			selectableChildren[selectedChild].UseKey(c);
		}

		public bool Selected { get; set; }
	}
}
