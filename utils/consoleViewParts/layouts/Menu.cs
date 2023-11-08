using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.views;

namespace JiraClone.utils.consoleViewParts.layouts
{
    public class Menu : Printable
    {
        private VerticalLayout layout;
        private List<Option> options;
        private int selectedOption;

        public Menu(int height, int width) : base(height, width)
        {
            layout = new VerticalLayout(height, width);
            options = new();
            selectedOption = -1;
        }

        public override void Print(int left, int top)
        {
            layout.Print(left, top);
            if (selectedOption == -1)
            {
                selectedOption = 0;
                options[selectedOption].Selected = true;
            }
        }

        public void AddOption(Option option)
        {
            options.Add(option);
            layout.Add(option);
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
