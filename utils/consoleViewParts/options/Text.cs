using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace JiraClone.utils.consoleViewParts.options
{
    public class Text : Printable
    {
        private readonly string text;

        public Text(int height, int width, string text) : base(height, width)
        {
            this.text = text;
        }

        public override void Print(int left, int top)
        {
            base.Print(left, top);
            Console.SetCursorPosition((_width - text.Length) / 2 + left, top);
            Console.WriteLine(text);
        }
    }
}
