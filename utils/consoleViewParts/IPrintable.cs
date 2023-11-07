using System;

namespace JiraClone.utils.consoleViewParts
{
    public interface IPrintable
    {
        public void Print(int left, int top);
        public int Width { get; }
    }
}
