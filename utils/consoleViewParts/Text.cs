using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace JiraClone.utils.consoleViewParts
{
	public class Text : Printable
	{
		private readonly string text;

		public Text(int width, string text) : base(width)
		{
			this.text = text;
		}

		public override void Print(int left, int top)
		{
			Console.SetCursorPosition((width - text.Length) / 2 + left, top);
			Console.WriteLine(text);
		}
	}
}
