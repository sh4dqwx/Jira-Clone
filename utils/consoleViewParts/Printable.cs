using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts
{
    public abstract class Printable : IPrintable
    {
        public abstract void Print();

		public void SetBounds(int left, int top, int height, int width)
		{
			Left = left;
			Top = top;
			Height = height;
			Width = width;
		}

		public int Left { get; set; }

		public int Top { get; set; }

		public virtual int Height { get; set; }

		public virtual int Width { get; set; }
	}
}
