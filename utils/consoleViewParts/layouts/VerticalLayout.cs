using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.layouts
{
    public class VerticalLayout : Layout
    {
        private int _visibleCount;
        private int _gapSize;
        public VerticalLayout(int height, int width, int visibleCount, int gapSize): base(height, width)
        {
            _visibleCount = visibleCount;
            _gapSize = gapSize;
        }

        public override void Print(int left, int top)
        {
            base.Print(left, top);
            if(Height + 4 >= Console.BufferHeight)
                Console.SetBufferSize(Console.BufferWidth, Height + 4);

            Console.SetCursorPosition(Left, Top);
            PrintBorders();

            int selectedIndex = selectedChild >= 0 ? selectedChild : 0;
            int startIndex = 0;
            if (selectedIndex - _visibleCount / 2 >= 0 && selectedIndex + _visibleCount / 2 + _visibleCount % 2 < children.Count)
                startIndex = selectedIndex - _visibleCount / 2;
            else if (selectedIndex - _visibleCount / 2 < 0)
                startIndex = 0;
            else if (selectedIndex + _visibleCount / 2 >= children.Count)
                startIndex = children.Count - _visibleCount - 1;

            if(startIndex > 1)
            {
                Console.SetCursorPosition(Left + (Width - 1) / 2, Top);
                Console.WriteLine("🡅");
            }

            for(int i = startIndex; i < _visibleCount; i++)
            {
                Printable child = children[i];
                Console.SetCursorPosition(Left + 1, Console.CursorTop);
                child.Print(Left + 1, Console.CursorTop);
                Console.SetCursorPosition(Left + 1, child.Top + child.Height);
                
                if(i + 1 < children.Count)
                    PrintSeparators();
            }

            if (startIndex < children.Count - _visibleCount - 1)
            {
                Console.SetCursorPosition(Left + (Width - 1) / 2, Top + Height - 2);
                Console.WriteLine("🡇");
            }
        }

        private void PrintBorders()
        {
            string line = new StringBuilder()
                .Append('+')
                .Append('-', Width - 2)
                .Append('+')
                .ToString();

            Console.SetCursorPosition(Left, Top);
            Console.WriteLine(line);

            for(int i = 1; i <= Height; i++)
            {
                Console.SetCursorPosition(Left, Top + i);
                Console.Write("|");
                Console.SetCursorPosition(Left + Width - 1, Top + i);
                Console.Write("|");
            }

            Console.SetCursorPosition(Left, Top + Height - 1);
            Console.WriteLine(line);
        }

        private void PrintSeparators()
        {
            string line = new StringBuilder()
                .Append('-', Width - 2)
                .ToString();

            int linesPrinted = 0;

            if (_gapSize > 0)
            {
                Console.SetCursorPosition(Left, Top + linesPrinted++);
                Console.WriteLine();
            }
            if (_gapSize > 1)
            {
                Console.SetCursorPosition(Left, Top + linesPrinted++);
                Console.WriteLine(line);
            }

            for(int i = 0; _gapSize > 2 && i < _gapSize - 2;  i++)
            {
                Console.SetCursorPosition(Left, Top + linesPrinted++);
                Console.WriteLine();
            }

            if (_gapSize > 2)
            {
                Console.SetCursorPosition(Left, Top + linesPrinted++);
                Console.WriteLine(line);
            }
            if (_gapSize > 1)
            {
                Console.SetCursorPosition(Left, Top + linesPrinted++);
                Console.WriteLine(line);
            }
        }

		public override void Add(Printable child)
		{
            if (Width < child.Width)
                throw new Exception("Child is too wide");
            base.Add(child);
		}

		public override void Remove(Printable child)
		{
			base.Remove(child);
		}
    }
}
