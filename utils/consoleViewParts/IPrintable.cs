using System;

namespace JiraClone.utils.consoleViewParts
{
    public interface IPrintable
    {
        public void Print(int left, int top);
        public int Left { get; }
		public int Top { get; }
		public int Height { get; }
		public int Width { get; }
    }
}
