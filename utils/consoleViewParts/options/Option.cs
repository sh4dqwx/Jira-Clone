using System;
using System.Text;

namespace JiraClone.utils.consoleViewParts.options
{
    public abstract class Option : Printable, IOption
    {
        private bool _selected;
        private string _error;
        private readonly string _name;
        public bool Selected { get => _selected; set { _selected = value; } }
        public string Error { get => _error; set => _error = value; }
        public string Name { get => _name; }

        public Option(string name) : base()
        {
            _name = name;
            _error = "";
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
	}
}
