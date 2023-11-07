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
        public VerticalLayout(int width) : base(width) { }

        public override void Print(int left, int top)
        {
            base.Print(left, top);
            Console.SetCursorPosition(left, top);
            foreach (var child in children)
            {
                int childWidth = child.Width;
                Console.CursorLeft = (width - childWidth) / 2 + left;
                child.Print(Console.CursorLeft, Console.CursorTop);
                Console.CursorTop++;
            }
        }
    }
}
