using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.options
{
	public class Button : Option
	{
		private int _startIndex;
		private int _nameStart;
		private int _nameEnd;
		private int _nameWidth;
		private Action _callback;

		public Button(string name, Action callback) : base(name)
		{
			Left = 0;
			Height = 5;
			Width = Constants.BUTTON_WIDTH;
			_startIndex = 0;
			_callback = callback;
		}

		public override void Print()
		{
			int cursorLeft = Left;
			int cursorTop = Top;
			if (Selected) Console.ForegroundColor = ConsoleColor.Cyan;
			else Console.ForegroundColor = ConsoleColor.White;
			Console.CursorVisible = false;

			base.Print();

			cursorTop += 2;
			Console.SetCursorPosition(_nameStart, cursorTop);
			Console.Write(Name.Substring(_startIndex, Math.Min(Name.Length, _nameWidth)));
			cursorTop++;

            if (Error.Length > 0)
            {
				int errorMarginLeft = (Width - Error.Length) / 2;
                Console.SetCursorPosition(cursorLeft + errorMarginLeft, cursorTop);

				ConsoleColor prevColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(Error);
				Console.ForegroundColor = prevColor;
            }
        }

		public override bool UseKey(ConsoleKeyInfo c)
		{
			if (c.Key == ConsoleKey.LeftArrow && _startIndex > 0)
			{
				_startIndex--;
				Print();
				return true;
			}
			else if (c.Key == ConsoleKey.RightArrow && _startIndex < Name.Length - _nameWidth)
			{
				_startIndex++;
				Print();
				return true;
			}
			else if(c.Key == ConsoleKey.Enter)
			{
				_callback();
				return true;
			}
			return false;
		}

		public override int Left
		{
			get => base.Left;
			set
			{
				base.Left = value;
				int margin = Math.Max(Constants.INPUT_MARGIN, (Width - Name.Length) / 2);
				_nameStart = Left + margin;
				_nameEnd = Left + Width - margin;
				_nameWidth = _nameEnd - _nameStart;
			}
		}
	}
}
