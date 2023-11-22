using System;
using System.Text;
using System.Globalization;
using System.Windows.Controls;

namespace JiraClone.utils.consoleViewParts.options
{
    public class Input : Option
    {
        private readonly StringBuilder valueBuilder = new();
        private readonly ValidationRule? _validationRule;
        private bool _isPassword;

        public string Value
        {
            get { return valueBuilder.ToString(); }
            set
            {
                valueBuilder.Clear();
                valueBuilder.Append(value);
            }
        }

        public bool IsPassword { get => _isPassword; }

        public Input(string name, bool isPassword = false, ValidationRule? validationRule = null) : base(name)
        {
            Height = 5;
            Width = Constants.BUTTON_WIDTH;
			_isPassword = isPassword;
			_validationRule = validationRule;
		}

        public override void UseKey(char c)
        {
            if (Console.CursorLeft < Left + Width - Constants.InputMargin - 1 && c >= 32 && c <= 127)
            {
                valueBuilder.Append(c);
                if (_isPassword) Console.Write('*');
                else Console.Write(c);
            }
            else if (Console.CursorLeft > Left + Constants.InputSpacer + Constants.InputMargin && c == '\b')
            {
                valueBuilder.Remove(valueBuilder.Length - 1, 1);
                Console.Write("\b \b");
            }
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

			int marginLeft = Math.Max(0, (Constants.InputSpacer - Name.Length) / 2);
            cursorTop += 2;
			Console.SetCursorPosition(cursorLeft + marginLeft, cursorTop);
            Console.Write(Name);
            cursorTop++;

            if (Error.Length > 0)
            {
                Console.SetCursorPosition(cursorLeft + Constants.InputSpacer + Constants.InputMargin, cursorTop);

				ConsoleColor prevColor = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("↑ " + Error + " ↑");
				Console.ForegroundColor = prevColor;
			}
            cursorTop--;

			Console.SetCursorPosition(cursorLeft + Constants.InputSpacer + Constants.InputMargin, cursorTop);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(_isPassword ? new StringBuilder().Append('*', valueBuilder.Length).ToString() : valueBuilder.ToString());
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
    }
}
