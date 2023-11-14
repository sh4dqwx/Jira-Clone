using System;
using System.Text;
using System.Globalization;
using System.Windows.Controls;

namespace JiraClone.utils.consoleViewParts.options
{
    public class Input : Option
    {
        private static readonly int InputSpacer = 20;

        private readonly StringBuilder valueBuilder = new();
        private readonly ValidationRule? _validationRule;
        private int _inputLeft = InputSpacer;
        private bool _isPassword;


        public Input(int height, int width, string name, bool isPassword = false, ValidationRule? validationRule = null) : base(height, width, name)
        {
			_isPassword = isPassword;
			_validationRule = validationRule;
		}

        public override void UseKey(char c)
        {
            if (Console.CursorLeft < _left + _width - 3 && c >= 32 && c <= 127)
            {
                valueBuilder.Append(c);
                if (_isPassword) Console.Write('*');
                else Console.Write(c);
            }
            if (Console.CursorLeft > _inputLeft && c == '\b')
            {
                valueBuilder.Remove(valueBuilder.Length - 1, 1);
                Console.Write("\b \b");
            }
        }

        public override void Print(int left, int top)
        {
            base.Print(left, top);
			_inputLeft = _left + InputSpacer + 2;

			if (Selected)
				Console.ForegroundColor = ConsoleColor.Cyan;

			Console.SetCursorPosition(left + ((InputSpacer - _name.Length) / 2), top + 2);
            Console.Write(_name);

            for(int i=0; i<5; i++)
            {
				Console.SetCursorPosition(left + InputSpacer - 1, top + i);
                if (i == 0 || i == 4) Console.Write('+');
                else Console.Write('|');
			}

            if(_error.Length > 0)
            {
                Console.SetCursorPosition(_inputLeft, top + 3);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(_error);
            }

            Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(_inputLeft, top + 2);
            Console.Write(_isPassword ? new StringBuilder().Append('*', valueBuilder.Length).ToString() : valueBuilder.ToString());


            if (Selected)
                Console.CursorVisible = true;
            else
                Console.CursorVisible = false;
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

            Error = (string)validationResult.ErrorContent;
            Print();
            return false;
        }

		public string Value
        {
            get { return valueBuilder.ToString(); }
        }
    }
}
