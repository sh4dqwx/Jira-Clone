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
			_isPassword = isPassword;
			_validationRule = validationRule;
		}

        public override void UseKey(char c)
        {
            if (Console.CursorLeft < Left + Width - 1 - Constants.InputMargin && c >= 32 && c <= 127)
            {
                valueBuilder.Append(c);
                if (_isPassword) Console.Write('*');
                else Console.Write(c);
            }
            else if (Console.CursorLeft > Constants.InputSpacer && c == '\b')
            {
                valueBuilder.Remove(valueBuilder.Length - 1, 1);
                Console.Write("\b \b");
            }
        }

        public override void Print()
        {
            Console.SetCursorPosition(Left + Constants.InputMargin, Top);
            if(Selected)
                Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(Name);

            Console.SetCursorPosition(Left + Constants.InputSpacer, Top);
            if(Selected)
                Console.ForegroundColor = ConsoleColor.White;
            Console.Write(_isPassword ? new StringBuilder().Append('*', valueBuilder.Length).ToString() : valueBuilder.ToString());

            if(Error.Length > 0)
            {
                Console.SetCursorPosition(Left + Constants.InputSpacer, Top + 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("↑ " + Error + " ↑");
            }

            Console.ForegroundColor = ConsoleColor.White;
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
