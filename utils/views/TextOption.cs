using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.views
{
	public class TextOption : ITextOption
	{
		public readonly Option option;
		private StringBuilder valueBuilder = new StringBuilder("");
		public readonly int inputX;
		public readonly int inputY;
		private bool isPassword;

		public string Value
		{
			get { return valueBuilder.ToString(); }
		}

		public TextOption(string title, int x, int y, bool isPassword = false)
		{
			option = new Option(title, x, y);
			inputX = x + 20;
			inputY = y;
			this.isPassword = isPassword;
		}

		public void Select()
		{
			option.Select();
			Console.SetCursorPosition(inputX + valueBuilder.Length, inputY);
			Console.ForegroundColor = ConsoleColor.White;
			Console.CursorVisible = true;
		}

		public void Unselect()
		{
			option.Unselect();
			Console.CursorVisible = false;
		}

		public void UseKey(char c)
		{
			if (c >= 32 && c <= 127)
			{
				valueBuilder.Append(c);
				if (isPassword) Console.Write('*');
				else Console.Write(c);
			}
			if (Console.CursorLeft > inputX && c == '\b')
			{
				valueBuilder.Remove(valueBuilder.Length - 1, 1);
				Console.Write("\b \b");
			}
		}

		public override string ToString()
		{
			return option.Title ?? "";
		}
	}
}
