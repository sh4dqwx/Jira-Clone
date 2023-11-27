using JiraClone.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.options
{
    public class TextField : Printable
    {
        private readonly string[] _value;
        public string[] Value { get => _value; }

        public TextField(string value, int height, int width) : base()
        {
            Height = height;
            Width = width;

            _value = value.Split(new[] { '\n' }, StringSplitOptions.None)
                .SelectMany(line =>
                    Enumerable.Range(0, (int)Math.Ceiling((double)line.Length / width))
                        .Select(i =>
                        {
                            int startIndex = i * (width - 2);
                            int length = Math.Min(width - 2, line.Length - startIndex);
                            return line.Substring(startIndex, length);
                        })
                        .ToArray()
                )
                .ToArray();

            if (_value.Length > height)
                throw new Exception("Too long value");
        }

        public override void Print()
        {
            Console.SetCursorPosition(Left, Top);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("+" + new string('-', Width - 2) + "+");

            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(Left, Top + i + 1);
                if (i < Value.Length)
                    Console.WriteLine("|" + Value[i].PadRight(Width - 2) + "|");
                else
                    Console.WriteLine("|" + new string(' ', Width - 2) + "|");
            }

            Console.SetCursorPosition(Left, Top + Height);
            Console.WriteLine("+" + new string('-', Width - 2) + "+");
        }

    }
}
