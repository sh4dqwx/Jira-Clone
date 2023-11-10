using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.layouts
{
    public class VerticalLayout : CompoundPrintable
    {
        public VerticalLayout(int height, int width) : base(height, width) { }

        public override void Print(int left, int top)
        {
            base.Print(left, top);
            Console.SetCursorPosition(left, top);
            foreach (var child in children)
            {
                child.Print((_width - child.Width) / 2 + left, Console.CursorTop);
                Console.SetCursorPosition(left, child.Top + child.Height + 1);
            }
        }

		public override void Add(Printable child)
		{
			base.Add(child);
            if (_height > 0) _height += 1; 
            _height += child.Height;
		}

		public override void Remove(Printable child)
		{
			base.Remove(child);
            _height -= child.Height;
            if (_height > 0) _height -= 1;
		}
	}
}
