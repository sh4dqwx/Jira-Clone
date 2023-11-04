using System;

namespace JiraClone.utils.consoleViewParts
{
    public class Option : Drawable, IOption
    {
		public readonly string title;
		public readonly int left;
		public readonly int top;

		public Option(string title, int left, int top)
		{
			this.title = title;
			this.left = left;
			this.top = top;
		}

		public string Title
		{
			get { return title; }
		}

		public void Select()
		{
			Console.SetCursorPosition(left, top);
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(title);
		}

		public void Unselect()
		{
			Console.SetCursorPosition(left, top);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(title);
		}

		public void UseKey(char c)
		{
			if (c == '\n' || c == '\r')
			{
				//Można dać jakąś funkcję onClick podaną przy tworzeniu
				Console.WriteLine("klik");
			}
		}
	}
}
