using System;
using System.Text;

namespace JiraClone.utils.consoleViewParts
{
    public class TextOption : IPrintable, ITextOption
	{
		private static readonly int InputSpacer = 20;

		private readonly StringBuilder valueBuilder = new();
		private readonly Option option;
		private readonly int inputLeft;
        private readonly int inputTop;
		private readonly bool isPassword;


		public TextOption(string title, int inputLeft, int inputTop, bool isPassword = false)
		{
			option = new Option(inputTop, title);
			this.inputLeft = inputLeft + InputSpacer;
			this.inputTop = inputTop;
			this.isPassword = isPassword;
		}

        public void Select()
		{
			option.Select();
			Console.SetCursorPosition(inputLeft + valueBuilder.Length, inputTop);
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
			if (Console.CursorLeft > inputLeft && c == '\b')
			{
				valueBuilder.Remove(valueBuilder.Length - 1, 1);
				Console.Write("\b \b");
			}
		}

		public void Print(int left, int top)
		{
			throw new NotImplementedException();
		}

		public string Value
		{
			get { return valueBuilder.ToString(); }
		}
	}
}
