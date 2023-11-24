using JiraClone.utils.consoleViewParts.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.layouts
{
	public class HorizontalMenuBar : Layout, ISelectable
	{
		private int _visibleCount;

		private int GetStartIndex()
		{
			if (selectedChild <= _visibleCount / 2) return 0;
			if (selectedChild >= children.Count - _visibleCount / 2)
				return children.Count - _visibleCount;
			return selectedChild - _visibleCount / 2;
		}

		public HorizontalMenuBar(int visibleCount): base()
		{
			Height = 4;
			Width = 5;
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

			if (selectedChild != -1) children[selectedChild].Print();
		}

		public override void Add(Printable child)
		{
			if (child is not VerticalMenu) throw new NotSupportedException();

			base.Add(child);
			Height = Math.Max(Height, child.Height); 
			if (children.Count > _visibleCount) return;
			if (children.Count > 1) Width++;
			Width += child.Width;
		}

		public override void Remove(Printable child)
		{
			if (child is not VerticalMenu) throw new NotSupportedException();

			base.Remove(child);
			if (children.Count == 0) Height = 4;
			if (children.Count > _visibleCount) return;
			if (children.Count > 1) Width--;
			Width -= child.Width;
		}

		public void UnselectSelected()
		{
			if (selectedChild == -1)
				return;

			((VerticalMenu)children[selectedChild]).UnselectSelected();
			((VerticalMenu)children[selectedChild]).Selected = false;
		}

		public bool SelectTop()
		{
			if (children.Count == 0)
				return false;

			UnselectSelected();

			selectedChild = 0;

			for (int i = 0; i < children.Count; i++, selectedChild = i)
			{
				VerticalMenu child = (VerticalMenu)children[i];
				if (child.SelectTop())
					return true;
			}

			return false;
		}

		public bool SelectBottom()
		{
			if (children.Count == 0)
				return false;

			UnselectSelected();

			selectedChild = children.Count - 1;

			for (int i = children.Count - 1; i >= 0; i--, selectedChild = i)
			{
				VerticalMenu child = (VerticalMenu)children[i];
				if (child.SelectBottom())
					return true;
			}

			return false;
		}

		public bool SelectNext()
		{
			if (selectedChild == -1)
				return false;

			UnselectSelected();
			if (selectedChild == children.Count - 1)
				return false;

			((VerticalMenu)children[++selectedChild]).SelectTop();
			Print();

			return true;
		}

		public bool SelectPrevious()
		{
			if (selectedChild == -1)
				return false;

			UnselectSelected();
			if (selectedChild <= 0)
				return false;

			((VerticalMenu)children[--selectedChild]).SelectTop();
			Print();

			return true;
		}

		public void UseKey(char c)
		{
			if (selectedChild < 0) return;
			((VerticalMenu)children[selectedChild]).UseKey(c);
		}

		public bool Selected { get; set; }
	}
}
