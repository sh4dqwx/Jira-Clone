using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.viewmodels;
using System;
using System.ComponentModel;

namespace JiraClone.views
{
    public class ProjectView: ConsoleView
    {
        private ProjectsViewModel viewModel;

        private VerticalMenu menu;
        private bool closeFlag = false;

        private void ResetView()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;

            Print();
            SelectTop();
        }

        public ProjectView(ProjectsViewModel viewModel)
        {
            this.viewModel = viewModel;
            viewModel.PropertyChanged += EventHandler;

            menu = new VerticalMenu(5);

            //Może horyzontalne menu
            menu.Add(new Button("Stwórz projekt", () => { }));
            menu.Add(new Button("Powrót", () => { closeFlag = true; }));

            Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(new Text("PROJEKTY"));
            Add(menu);
        }

        private void EventHandler(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(e.PropertyName);
        }

        public void Start()
        {
            ResetView();

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectableChildren[selectedChild] is VerticalMenu)
                            SelectPrevious();
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectableChildren[selectedChild] is VerticalMenu)
                            SelectNext();
                        break;

                    case ConsoleKey.LeftArrow:
                        if (selectableChildren[selectedChild] is HorizontalMenu)
                            SelectPrevious();
                        break;

                    case ConsoleKey.RightArrow:
                        if (selectableChildren[selectedChild] is HorizontalMenu)
                            SelectNext();
                        break;

                    default:
                        UseKey(keyInfo.KeyChar);
                        break;
                }


                if (closeFlag)
                {
                    closeFlag = false;
                    ResetView();
                    return;
                }
            }
        }
    }
}
