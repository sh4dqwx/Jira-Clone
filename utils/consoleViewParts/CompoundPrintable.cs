using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts
{
	public abstract class CompoundPrintable : Printable
	{
		protected List<Printable> children;

		protected CompoundPrintable(int width) : base(width)
		{
			children = new List<Printable>();
		}

		protected void Add(Printable child) { children.Add(child); }

		protected void Remove(Printable child) { children.Remove(child); }
	}
}
