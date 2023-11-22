using System;

namespace JiraClone.utils.consoleViewParts
{
    public interface IPrintable
    {
        public void Print();
        public void SetBounds(int left, int top, int height, int width);
        public int Left { get; set; }
		public int Top { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }
    }
}
