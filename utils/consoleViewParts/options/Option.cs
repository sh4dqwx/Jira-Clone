using System;
using System.Text;

namespace JiraClone.utils.consoleViewParts.options
{
    public abstract class Option : Printable, IOption
    {
        private bool _selected;
        private Action _callback;
        protected readonly string _name;
        public bool Selected { get => _selected; set { _selected = value; Print(); } }
        public Action Callback { get => _callback; }

        public Option(int width, string name, Action callback) : base(width)
        {
            _name = name;
            _callback = callback;
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
            base.Print(left, top); 

            if(Selected)
                Console.ForegroundColor = ConsoleColor.Cyan;

            Console.SetCursorPosition(left, top);
            Console.WriteLine(new StringBuilder()
                .Append('+')
                .Append('-', width - 2)
                .Append('+')
                .ToString()
            );

            for(int i=0; i<3; i++)
            {
				Console.CursorLeft = left;
				Console.WriteLine(new StringBuilder()
					.Append('|')
					.Append(' ', width - 2)
					.Append('|')
					.ToString()
				);
			}

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
