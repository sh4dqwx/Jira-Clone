using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace JiraClone.utils.consoleViewParts
{
    public class VerticalLayout : CompoundPrintable
    {
        public VerticalLayout(int width) : base(width) { }

		public override void Print(int left, int top)
		{
			Console.SetCursorPosition(left, top);
			foreach (var child in children)
			{
				child.Print(Console.CursorLeft, Console.CursorTop);
				Console.CursorTop++;
			}
		}
	}
}
