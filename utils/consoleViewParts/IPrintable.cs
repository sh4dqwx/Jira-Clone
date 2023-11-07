using System;

namespace JiraClone.utils.consoleViewParts
{
    public interface IPrintable
    {
        public void Print(int left, int top);
		//protected void PrintCenter(string text)
		//{
		//    int margin = (Console.WindowWidth - text.Length) / 2;
		//    Console.SetCursorPosition(margin, Console.CursorTop);
		//    Console.WriteLine(text);
		//}
	}
}
