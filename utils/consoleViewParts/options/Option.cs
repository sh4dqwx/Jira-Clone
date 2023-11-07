using System;
using System.Text;

namespace JiraClone.utils.consoleViewParts.options
{
    public class Option : Printable, IOption
    {
        private bool _selected;
        private readonly string name;
        public bool Selected { get => _selected; set { _selected = value; Print(); } }
        public Action Callback { get; set; }

        public Option(int width, string name, Action callback) : base(width)
        {
            this.name = name;
            this.Callback = callback;
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
            int emptyLane = width - 2;
            int nameLaneLeft = (emptyLane - name.Length) / 2 - 4;
            int nameLaneRight = nameLaneLeft + (emptyLane - name.Length) % 2;

            if(Selected)
                Console.ForegroundColor = ConsoleColor.Cyan;

            Console.SetCursorPosition(left, top);
            Console.WriteLine(new StringBuilder()
                .Append('+')
                .Append('-', emptyLane)
                .Append('+')
                .ToString()
            );
            Console.CursorLeft = left;
            Console.WriteLine(new StringBuilder()
                .Append('|')
                .Append(' ', emptyLane)
                .Append('|')
                .ToString()
            );
            Console.CursorLeft = left;
            Console.WriteLine(new StringBuilder()
                .Append('|')
                .Append(' ', nameLaneLeft)
                .Append(Selected ? "->  " : " *  ")
                .Append(name)
                .Append(Selected ? "  <-" : "  * ")
                .Append(' ', nameLaneRight)
                .Append('|')
                .ToString()
            );

            Console.CursorLeft = left;
            Console.WriteLine(new StringBuilder()
                .Append('|')
                .Append(' ', emptyLane)
                .Append('|')
                .ToString()
            );

            Console.CursorLeft = left;
            Console.WriteLine(new StringBuilder()
                .Append('+')
                .Append('-', emptyLane)
                .Append('+')
                .ToString()
            );

            if (Selected)
                Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
