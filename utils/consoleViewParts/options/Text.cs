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
        private readonly string _name;
        public string Name { get => _name; }

        public Text(string name) : base()
        {
            _name = name;
            Height = 1;
            Width = name.Length;
        }

        public override void Print()
        {
            int marginLeft = (Width - Name.Length) / 2;
            Console.SetCursorPosition(Left + marginLeft, Top);
            Console.Write(Name);   
        }
    }
}
