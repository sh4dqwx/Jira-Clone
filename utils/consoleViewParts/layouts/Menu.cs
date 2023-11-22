using JiraClone.utils.consoleViewParts.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.layouts
{
	public abstract class Menu : Layout, ISelectable
	{
		protected int _visibleCount;

		protected int GetStartIndex()
		{
			if (selectedChild <= _visibleCount / 2) return 0;
			if (selectedChild >= children.Count - _visibleCount / 2)
				return children.Count - _visibleCount;
			return selectedChild - _visibleCount / 2;
		}

		public bool SelectPrevious()
		{
			if (selectedChild <= 0)
				return false;

			UnselectSelected();
			((Option)children[--selectedChild]).Selected = true;
			Print();

			return true;
		}

		public bool SelectNext()
		{
			if (selectedChild == -1 || selectedChild == children.Count - 1)
				return false;

			UnselectSelected();
			((Option)children[++selectedChild]).Selected = true;
			Print();
			return true;
		}

		public void SelectTop()
		{
			if (selectedChild != -1) UnselectSelected();

			selectedChild = 0;
			((Option)children[selectedChild]).Selected = true;
			Print();
		}

		public void SelectBottom()
		{
			if (selectedChild != -1) UnselectSelected();

			selectedChild = children.Count - 1;
			((Option)children[selectedChild]).Selected = true;
			Print();
		}

		public void UnselectSelected()
		{
			if (selectedChild == -1)
				return;

			((Option)children[selectedChild]).Selected = false;
		}

		public void UseKey(char c)
		{
			if (selectedChild < 0) return;
			((Option)children[selectedChild]).UseKey(c);
		}

		public bool Selected { get; set; }
	}
}
