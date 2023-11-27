using System;
using System.Text;
using System.Globalization;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Windows.Input;

namespace JiraClone.utils.consoleViewParts.options
{
    public class Input : Option
    {
        private readonly StringBuilder valueBuilder = new();
        private readonly ValidationRule? _validationRule;
        private bool _isPassword;
        private int _inputStart;
        private int _inputEnd;
        private int _inputWidth;
        private int _startIndex;
        private int _endIndex;

        public Input(string name, bool isPassword = false, ValidationRule? validationRule = null) : base(name)
        {
            Left = 0;
            Height = 5;
            Width = Constants.BUTTON_WIDTH;
			_isPassword = isPassword;
			_validationRule = validationRule;
            _startIndex = 0;
            _endIndex = 0;
		}

        public override bool UseKey(ConsoleKeyInfo c)
        {
            if (c.Key == ConsoleKey.LeftArrow)
            {
                if (Console.CursorLeft > _inputStart)
                {
                    Console.CursorLeft--;
                    return true;
                }
                else if (Console.CursorLeft == _inputStart && _startIndex > 0)
                {
                    _startIndex--;
					PrintValue(modified: true);
					return true;
                }
            }
            else if (c.Key == ConsoleKey.RightArrow)
            {
                if (Console.CursorLeft < _inputEnd)
                {
					Console.CursorLeft++;
                    return true;
				}
                else if (Console.CursorLeft == _inputEnd && Value.Length - _startIndex > _endIndex)
                {
                    _startIndex++;
					PrintValue(modified: true);
					return true;
                } 
            }
            else if (c.KeyChar >= 32 && c.KeyChar <= 127)
            {
                int position = Console.CursorLeft - _inputStart + _startIndex;
                valueBuilder.Insert(position, c.KeyChar);
				if (Console.CursorLeft < _inputEnd && _startIndex == 0) Console.CursorLeft++;

				if (_endIndex < _inputWidth - 1) _endIndex++;
                else _startIndex++;
                
                PrintValue(modified: true);
                return true;
            }
            else if (Console.CursorLeft > _inputStart && c.Key == ConsoleKey.Backspace)
            {
                int position = Console.CursorLeft - _inputStart + _startIndex;
                valueBuilder.Remove(position - 1, 1);
				if (Console.CursorLeft < _inputEnd || _startIndex == 0) Console.CursorLeft--;

				if (Value.Length < _endIndex) _endIndex--;
                else _startIndex--;

                PrintValue(modified: true);
                return true;
            }
            return false;
        }

        private void PrintValue(bool modified = false)
        {
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;

            Console.CursorVisible = false;

			Console.SetCursorPosition(_inputStart, Top + 2);
			for (int i = _inputStart; i < _inputEnd; i++) Console.Write(' ');

            Console.SetCursorPosition(_inputStart, Top + 2);
            if (_isPassword) Console.Write(new StringBuilder().Append('*', _endIndex - _startIndex));
			else Console.Write(Value.Substring(_startIndex, _endIndex));

			if (modified) Console.SetCursorPosition(cursorLeft, cursorTop);
            else Console.SetCursorPosition(_inputStart + _endIndex, Top + 2);
			
            Console.CursorVisible = true;
		}

        public override void Print()
        {
            int cursorLeft = Left;
            int cursorTop = Top;
            if (Selected) Console.ForegroundColor = ConsoleColor.Cyan;
            else Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;

            base.Print();

			for (int i = 0; i < Height; i++)
			{
				Console.SetCursorPosition(cursorLeft + Constants.InputSpacer - 1, Top + i);
				if (i == 0 || i == Height - 1) Console.Write('+');
				else Console.Write('|');
			}

            cursorTop += 2;
			int marginLeft = Math.Max(0, (Constants.InputSpacer - Name.Length) / 2);
			Console.SetCursorPosition(cursorLeft + marginLeft, cursorTop);
            Console.Write(Name);
            cursorTop++;

            if (Error.Length > 0)
            {
                Console.SetCursorPosition(cursorLeft + Constants.InputSpacer + Constants.InputMargin, cursorTop);

				ConsoleColor prevColor = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(Error);
				Console.ForegroundColor = prevColor;
			}
            cursorTop--;

			Console.SetCursorPosition(cursorLeft + Constants.InputSpacer + Constants.InputMargin, cursorTop);
			Console.ForegroundColor = ConsoleColor.White;
            PrintValue();
            if (Selected) Console.CursorVisible = true;
		}

        public bool Validate()
        {
            if (_validationRule == null) return true;

            ValidationResult validationResult = _validationRule.Validate(Value, CultureInfo.CurrentCulture);
            if (validationResult == ValidationResult.ValidResult)
            {
                Error = "";
                Print();
                return true;
            }
            else
            {
                Error = (string)validationResult.ErrorContent;
                Print();
                return false;
            }
        }

        public void Clear()
        {
            valueBuilder.Clear();
            Error = "";
        }

		public override int Left
        {
            get => base.Left;
            set
            {
                base.Left = value;
				_inputStart = Left + Constants.InputSpacer + Constants.InputMargin;
				_inputEnd = Left + Width - Constants.InputMargin - 2;
				_inputWidth = _inputEnd - _inputStart + 1;
			}
        }

		public string Value
		{
            get => valueBuilder.ToString();
			set
			{
				valueBuilder.Clear();
				valueBuilder.Append(value);
			}
		}

		public bool IsPassword { get => _isPassword; }
	}
}
