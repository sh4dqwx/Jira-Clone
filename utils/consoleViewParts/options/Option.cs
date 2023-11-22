using System;
using System.Text;

namespace JiraClone.utils.consoleViewParts.options
{
    public abstract class Option : Printable, IOption
    {
        private readonly string _name;

        public Option(string name) : base()
        {
            _name = name;
            Error = "";
        }

		public override void Print()
		{
			int cursorLeft = Left;
			int cursorTop = Top;

			Console.SetCursorPosition(cursorLeft, cursorTop);
			Console.Write(new StringBuilder()
				.Append('+')
				.Append('-', Width - 2)
				.Append('+'));
			cursorTop++;

			for(int i=0; i<3; i++)
			{
				Console.SetCursorPosition(cursorLeft, cursorTop);
				Console.Write(new StringBuilder()
					.Append('|')
					.Append(' ', Width - 2)
					.Append('|'));
				cursorTop++;
			}

			Console.SetCursorPosition(cursorLeft, cursorTop);
			Console.Write(new StringBuilder()
				.Append('+')
				.Append('-', Width - 2)
				.Append('+'));
		}

		public abstract void UseKey(char c);

		public bool Selected { get; set; }
		public string Error { get; set; }
		public string Name { get => _name; }
	}
}
