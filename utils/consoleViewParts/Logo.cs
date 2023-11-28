using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts
{
    public class Logo : Printable
    {
        private int currentLeft;
        private bool currentDirection;
        private readonly string[] logo = new string[]
        {
            @"  _____                            ___         __      ",
            @" /\___ \  __                     /'___`\     /'__`\    ",
            @" \/__/\ \/\_\  _ __    __       /\_\ /\ \   /\ \/\ \   ",
            @"    _\ \ \/\ \/\`'__\/'__`\     \/_/// /__  \ \ \ \ \  ",
            @"   /\ \_\ \ \ \ \ \//\ \L\.\_      // /_\ \__\ \ \_\ \ ",
            @"   \ \____/\ \_\ \_\\ \__/.\_\    /\______/\_\\ \____/ ",
            @"    \/___/  \/_/\/_/ \/__/\/_/    \/_____/\/_/ \/___/  ",
        };

        public Logo() : base()
        {
            Height = logo.Length;
            Width = logo[0].Length;
            currentLeft = Left;
            currentDirection = true;
        }

		public override void Print()
		{
			for (int i=0; i<logo.Length; i++)
            {
                Console.SetCursorPosition(currentLeft, Top + i);
                Console.WriteLine(logo[i]);
			}
		}

        public void ShiftToSide()
        {
            bool cursorViibility = Console.CursorVisible;
            Console.CursorVisible = false;
            (int left, int top) = Console.GetCursorPosition();
            ConsoleColor consoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            if (currentDirection)
            {
                if(Math.Abs(currentLeft - Left) >= Constants.MOVEMENT_LIMIT)
                {
                    currentDirection = false;
                    currentLeft--;
                }
                else currentLeft++;
            }
            else
            {
                if(Math.Abs(currentLeft + Constants.MOVEMENT_LIMIT) <= Left)
                {
                    currentDirection = true;
                    currentLeft++;
                }
                else currentLeft--;
            }
            Print();
            Console.ForegroundColor = consoleColor;
            Console.SetCursorPosition(left, top);
            Console.CursorVisible = cursorViibility;
        }

        public override int Left
        {
            get => base.Left;
            set
            {
                if (base.Left == value)
                    return;

                int diffrence = currentLeft - Left;
                currentLeft = value + diffrence;
                base.Left = value;
            }
        }
    }
}
