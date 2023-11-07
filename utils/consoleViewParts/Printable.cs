using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts
{
	public abstract class Printable : IPrintable
	{
		protected int width;

		protected Printable(int width)
		{
			this.width = width;
		}

		public abstract void Print(int left, int top);
	}
}
