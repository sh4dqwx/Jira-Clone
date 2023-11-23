using JiraClone.db.dbmodels;
using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.viewmodels;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace JiraClone.views
{
    public class ProjectsView: ConsoleView
    {
        private ProjectsViewModel viewModel;

        private VerticalMenu projectsMenu, bottomMenu;
        private HorizontalMenu actionMenu;
        private bool closeFlag = false;

        private void ResetView()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;

            projectsMenu.Clear();
            List<Project> projects = viewModel.GetProjects();
            foreach (var project in projects)
                projectsMenu.Add(new Button(project.Name, () => onProjectClick(project)));

            Print();
            SelectTop();
        }

        public ProjectsView(ProjectsViewModel viewModel, AddProjectView addProjectView, RemoveProjectView deleteProjectView)
        {
            this.viewModel = viewModel;
            viewModel.PropertyChanged += EventHandler;

            projectsMenu = new VerticalMenu(2);

            List<Project> projects = viewModel.GetProjects();
            foreach (var project in projects)
                projectsMenu.Add(new Button(project.Name, () => onProjectClick(project)));

            actionMenu = new HorizontalMenu(2);
            actionMenu.Add(new Button("Stwórz projekt", () => { addProjectView.Start(); ResetView(); }));
            actionMenu.Add(new Button("Usuń projekt", () => { deleteProjectView.Start(); ResetView(); }));
            actionMenu.Add(new Button("Udostępnij projekt", () => { }));

            bottomMenu = new VerticalMenu(1);
            bottomMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

            Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(new Text("PROJEKTY"));
            Add(projectsMenu);
            Add(actionMenu);
            Add(bottomMenu);
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
                        else if (selectableChildren[selectedChild] is HorizontalMenu)
                        {
                            selectableChildren[selectedChild].SelectTop();
                            SelectPrevious();
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectableChildren[selectedChild] is VerticalMenu)
                            SelectNext();
                        else if (selectableChildren[selectedChild] is HorizontalMenu)
                        {
                            selectableChildren[selectedChild].SelectBottom();
                            SelectNext();
                        }
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
