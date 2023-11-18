using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts
{
    public abstract class Printable : IPrintable
    {
        private int _left = 0;
        private int _top = 0;
        private int _width;
        private int _height;

        protected Printable(int height, int width)
        {
            _height = height;
            _width = width;
        }

        public virtual void Print(int left, int top)
        {
            _left = left;
            _top = top;
        }

        public void Refresh()
        {
            Print(Left, Top);
        }

		public int Left { get => _left; }

		public int Top { get => _top; }

		public int Height { get => _height; set => _height = value; }

		public int Width { get => _width; set => _width = value; }
	}
}
