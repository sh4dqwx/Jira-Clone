using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts
{
    public abstract class Printable : IPrintable
    {
        protected int _left = 0;
        protected int _top = 0;
        protected int _width;
        protected int _height;

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
    
        public void Print() { Print(_left, _top); }

		public int Left { get => _left; }

		public int Top { get => _top; }

		public int Height { get => _height; }

		public int Width { get => _width; }
	}
}
