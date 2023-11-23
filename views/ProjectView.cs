using JiraClone.db.dbmodels;
using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.viewmodels;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace JiraClone.views
{
    public class ProjectView: ConsoleView
    {
        private ProjectsViewModel viewModel;

        private VerticalMenu menu;
        private HorizontalMenu bottomMenu;
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

            List<Project> projects = viewModel.GetProjects();
            foreach (var project in projects)
                menu.Add(new Button(project.Name, () => onProjectClick(project)));

            bottomMenu = new HorizontalMenu(2);
            menu.Add(new Button("Stwórz projekt", () => { }));
            menu.Add(new Button("Powrót", () => { closeFlag = true; }));
            //TODO Naprawienie navigacji między menu i gdy menu jest puste
            //bottomMenu.Add(new Button("Stwórz projekt", () => { }));
            //bottomMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

            Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(new Text("PROJEKTY"));
            Add(menu);
            //Add(bottomMenu);
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

        private void onProjectClick(Project project)
        {
            //Przechodzenie do kolejnego widoku
            ResetView();
        }
    }
}
