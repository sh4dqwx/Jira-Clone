using System;
using System.Text;

namespace JiraClone.utils.consoleViewParts.options
{
    public abstract class Option : Printable, IOption
    {
        protected bool _selected;
        protected readonly string _name;
        public bool Selected { get => _selected; set { _selected = value; Print(); } }

        public Option(int height, int width, string name) : base(height, width)
        {
            _name = name;
        }

        public override void Print(int left, int top)
        {
            base.Print(left, top); 

            if(Selected)
                Console.ForegroundColor = ConsoleColor.Cyan;

            Console.SetCursorPosition(left, top);
            Console.WriteLine(new StringBuilder()
                .Append('+')
                .Append('-', _width - 2)
                .Append('+')
                .ToString()
            );

            for(int i=0; i<3; i++)
            {
				Console.CursorLeft = left;
				Console.WriteLine(new StringBuilder()
					.Append('|')
					.Append(' ', _width - 2)
					.Append('|')
					.ToString()
				);
			}

            Console.CursorLeft = left;
            Console.WriteLine(new StringBuilder()
                .Append('+')
                .Append('-', _width - 2)
                .Append('+')
                .ToString()
            );

            Console.ForegroundColor = ConsoleColor.White;
        }

        public abstract void UseKey(char c);
	}
}
