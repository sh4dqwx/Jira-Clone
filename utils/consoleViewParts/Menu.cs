using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts
{
    public class Menu : Drawable
    {
        private readonly IOption[] options;
    
        public Menu(IOption[] options)
        {
            this.options = options;
        }

        public void Print()
        {
            string line = new StringBuilder()
                .Append('+')
                .Append('-', Constants.MENU_WIDTH)
                .Append('+')
                .ToString();

            string emptyLine = new StringBuilder()
                .Append('|')
                .Append(' ', Constants.MENU_WIDTH)
                .Append('|')
                .ToString();

            foreach (var option in options)
            {
				PrintCenter(line);

                (int leftPosition, int topPosition) = Console.GetCursorPosition();
                Console.SetCursorPosition(0, topPosition + 1);
                PrintCenter(option.Title);
                Console.SetCursorPosition(leftPosition, topPosition);

                for (int i=0; i<3; i++) PrintCenter(emptyLine);

			}
            PrintCenter(line);
        }
    }
}
