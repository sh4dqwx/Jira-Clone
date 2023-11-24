using JiraClone.utils.consoleViewParts.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.layouts
{
	public abstract class Menu : Layout, IMenu
	{
		protected int _visibleCount;

		protected int GetStartIndex()
		{
			if (selectedChild <= _visibleCount / 2) return 0;
			if (selectedChild >= children.Count - _visibleCount / 2)
				return children.Count - _visibleCount;
			return selectedChild - _visibleCount / 2;
		}

		public override void Add(Printable child)
		{
			if (child is not Option) throw new NotSupportedException();
			base.Add(child);
		}

		public override void Remove(Printable child)
		{
			if (child is not Option) throw new NotSupportedException();
			base.Remove(child);
		}

		public bool SelectPrevious()
		{
            if (selectedChild == -1)
                return false;

			if (selectedChild <= 0)
				return false;

			UnselectSelected();
			((Option)children[--selectedChild]).Selected = true;
			Print();

			return true;
		}

		public bool SelectNext()
		{
			if (selectedChild == -1)
				return false;

			if (selectedChild == children.Count - 1)
				return false;

			UnselectSelected();
			((Option)children[++selectedChild]).Selected = true;
			Print();

			return true;
		}

		public bool SelectTop()
		{
			if (children.Count == 0)
				return false;

			if (selectedChild != -1) UnselectSelected();

			selectedChild = 0;
			((Option)children[selectedChild]).Selected = true;
			Print();
			return true;
		}

		public bool SelectBottom()
		{
            if (children.Count == 0)
                return false;

            if (selectedChild != -1) UnselectSelected();

			selectedChild = children.Count - 1;
			((Option)children[selectedChild]).Selected = true;
			Print();
			return true;
		}

		public void UnselectSelected()
		{
			if (selectedChild == -1)
				return;

			((Option)children[selectedChild]).Selected = false;
			Print();
		}

		public virtual bool UseKey(ConsoleKeyInfo c)
		{
			return ((ISelectable)children[selectedChild]).UseKey(c);
		}

		public bool CanSelect()
		{
			if (children.Count > 0) return true;
			return false;
		}

		public bool Selected { get; set; }
	}
}
