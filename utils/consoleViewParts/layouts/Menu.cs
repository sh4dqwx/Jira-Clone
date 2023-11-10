using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.views;

namespace JiraClone.utils.consoleViewParts.layouts
{
    public class Menu : VerticalLayout
    {
        private List<Option> options;
        private int selectedOption;

        public Menu(int height, int width) : base(height, width)
        {
            options = new();
            selectedOption = 0;
        }

        public override void Print(int left, int top)
        {
            base.Print(left, top);
            //if (selectedOption == -1)
            //{
            //    selectedOption = 0;
            //    options[selectedOption].Selected = true;
            //}
        }

        public override void Add(Printable child)
        {
            if (child is not Option) throw new ArgumentException("Argument is not Option class");
			base.Add(child);
			options.Add((Option)child);
        }

        public void NavigateTop()
        {
            options[selectedOption].Selected = false;
            options[selectedOption = 0].Selected = true;
        }

        public void NavigateUp()
        {
            if (selectedOption == 0) return;
            options[selectedOption--].Selected = false;
            options[selectedOption].Selected = true;
        }

        public void NavigateDown()
        {
			if (selectedOption == options.Count - 1) return;
			options[selectedOption++].Selected = false;
			options[selectedOption].Selected = true;
		}

        public void UseKey(char c)
        {
            options[selectedOption].UseKey(c);
        }
    }
}