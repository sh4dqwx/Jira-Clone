using System;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace JiraClone.utils.consoleViewParts
{
    public class Option : Printable, IOption
    {
		private readonly string name;

		public Option(int width, string name) : base(width)
		{
			this.name = name;
		}

		public void Select()
		{
			//Console.SetCursorPosition(left, top);
			//Console.ForegroundColor = ConsoleColor.Cyan;
			//Console.Write(title);
		}

		public void Unselect()
		{
			//Console.SetCursorPosition(left, top);
			//Console.ForegroundColor = ConsoleColor.White;
			//Console.Write(title);
		}

		public void UseKey(char c)
		{
			//if (c == '\n' || c == '\r')
			//{
			//	//Można dać jakąś funkcję onClick podaną przy tworzeniu
			//	Console.WriteLine("klik");
			//}
		}

		public override void Print(int left, int top)
		{
			Console.SetCursorPosition(left, top);
			Console.WriteLine(new StringBuilder()
				.Append('+')
				.Append('-', width - 2)
				.Append('+')
				.ToString()
			);
			Console.CursorLeft = left;
			Console.WriteLine(new StringBuilder()
				.Append('|')
				.Append(' ', width - 2)
				.Append('|')
				.ToString()
			);
			Console.CursorLeft = left;
			Console.WriteLine(new StringBuilder()
				.Append('|')
				.Append(' ', (width - 2 - name.Length) / 2 + 1)
				.Append(name)
				.Append(' ', (width - 2 - name.Length) / 2)
				.Append('|')
				.ToString()
			);
			Console.CursorLeft = left;
			Console.WriteLine(new StringBuilder()
				.Append('|')
				.Append(' ', width - 2)
				.Append('|')
				.ToString()
			);
			Console.CursorLeft = left;
			Console.WriteLine(new StringBuilder()
				.Append('+')
				.Append('-', width - 2)
				.Append('+')
				.ToString()
			);
		}
	}
}
