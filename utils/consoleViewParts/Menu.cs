using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts
{
	public class Menu : Printable
	{
		private VerticalLayout layout;

		public Menu(int width) : base(width)
		{
			layout = new VerticalLayout(width);
		}

		public override void Print(int left, int top)
		{
			throw new NotImplementedException();
		}
	}
}
