using System;

namespace JiraClone.utils.consoleViewParts
{
    public class Drawable
    {
        protected void PrintCenter(string text)
        {
            int margin = (Console.WindowWidth - text.Length) / 2;
            Console.SetCursorPosition(margin, Console.CursorTop);
            Console.WriteLine(text);
        }
    }
}
