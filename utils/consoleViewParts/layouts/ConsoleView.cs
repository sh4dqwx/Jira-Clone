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
		private List<ISelectable> selectableChildren;

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

		public void SelectTop()
		{
			UnselectSelected();

			selectedChild = 0;
			if (selectableChildren[selectedChild] is Option)
			{
				selectableChildren[selectedChild].Selected = true;
				((Printable)selectableChildren[selectedChild]).Print();
			}
			else selectableChildren[selectedChild].SelectTop();
		}

		public void SelectBottom()
		{
			UnselectSelected();

			selectedChild = selectableChildren.Count - 1;
			if (selectableChildren[selectedChild] is Option)
			{
				selectableChildren[selectedChild].Selected = true;
				((Printable)selectableChildren[selectedChild]).Print();
			}
			else selectableChildren[selectedChild].SelectTop();
		}

		public bool SelectUp()
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
				bool result = selectableChildren[selectedChild].SelectUp();
				if (result == true) return true;
				if (selectedChild <= 0) return false;
				selectableChildren[--selectedChild].SelectBottom();
				return true;
			}
		}

		public bool SelectDown()
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
				bool result = selectableChildren[selectedChild].SelectDown();
				if (result == true) return true;
				if (selectedChild >= selectableChildren.Count - 1) return false;
				selectableChildren[++selectedChild].SelectTop();
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
