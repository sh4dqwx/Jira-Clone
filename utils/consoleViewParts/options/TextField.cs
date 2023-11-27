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
        private string[] _value;

        public string? Value
        {
            get => _value != null ? string.Join('\n', _value) : null;
            set
            {
                if (value == null)
                    return;

                _value = value.Split(new[] { '\n' }, StringSplitOptions.None)
                    .SelectMany(line =>
                        Enumerable.Range(0, (int)Math.Ceiling((double)line.Length / Width))
                            .Select(i =>
                            {
                                int startIndex = i * (Width - 2);
                                int length = Math.Min(Width - 2, line.Length - startIndex);
                                return line.Substring(startIndex, length);
                            })
                            .ToArray()
                    )
                    .ToArray();

                if (_value.Length > Height)
                    throw new Exception("Too long value");
            }
        }

        public TextField(string value, int height, int width) : base()
        {
            Height = height;
            Width = width;
            Value = value;
        }

        public override void Print()
        {
            Console.SetCursorPosition(Left, Top);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("+" + new string('-', Width - 2) + "+");

            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(Left, Top + i + 1);
                if (i < _value.Length)
                    Console.WriteLine("|" + _value[i].PadRight(Width - 2) + "|");
                else
                    Console.WriteLine("|" + new string(' ', Width - 2) + "|");
            }

            Console.SetCursorPosition(Left, Top + Height);
            Console.WriteLine("+" + new string('-', Width - 2) + "+");
        }
    }
}
