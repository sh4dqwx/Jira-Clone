using JiraClone.db.dbmodels;
using JiraClone.utils;
using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.viewmodels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace JiraClone.views.ProjectViews
{
    public class ProjectsView : ConsoleView
    {
        private ProjectsViewModel viewModel;
        private ProjectDetailsView projectDetailsView;

        private VerticalMenu projectsMenu;
        private HorizontalMenu actionMenu, bottomMenu;
        private bool closeFlag = false;

        private void OnProjectsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            projectsMenu.ClearChildren();
            List<Project> ownedProjects = viewModel.OwnedProjectList.ToList() ?? new();
            foreach (var project in ownedProjects)
                projectsMenu.Add(new Button(project.Name, () => StartNewConsoleView(() => projectDetailsView.Start(project))));

            List<Project> sharedProjects = viewModel.SharedProjectList.ToList() ?? new();
            foreach (var project in sharedProjects)
                projectsMenu.Add(new Button("(SHARED) " + project.Name, () => StartNewConsoleView(() => projectDetailsView.Start(project))));
        }

        protected override void ResetView()
        {
            Clear();

            base.ResetView();
        }

        public ProjectsView(
            ProjectsViewModel projectsViewModel,
            AddProjectView addProjectView,
            RemoveProjectView deleteProjectView,
            ShareProjectView shareProjectView,
            RevokeProjectView revokeProjectView,
            ProjectDetailsView projectDetailsView
        ) {
            this.projectDetailsView = projectDetailsView;

            viewModel = projectsViewModel;
            viewModel.OwnedProjectList.CollectionChanged += OnProjectsChanged!;
            viewModel.SharedProjectList.CollectionChanged += OnProjectsChanged!;

            projectsMenu = new VerticalMenu("PROJEKTY", 3);

            actionMenu = new HorizontalMenu(2);
            actionMenu.Add(new Button("Stwórz projekt", () => { StartNewConsoleView(addProjectView.Start); }));
            actionMenu.Add(new Button("Usuń projekt", () => { StartNewConsoleView(deleteProjectView.Start); }));
            actionMenu.Add(new Button("Udostępnij projekt", () => { StartNewConsoleView(shareProjectView.Start); }));
            actionMenu.Add(new Button("Odbierz projekt", () => { StartNewConsoleView(revokeProjectView.Start); }));

            bottomMenu = new HorizontalMenu(1);
            bottomMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

            Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(projectsMenu);
            Add(actionMenu);
            Add(bottomMenu);
        }

        public Func<object>? Start()
        {
            viewModel.GetOwnedProjects();
            viewModel.GetSharedProjects();
            ResetView();
			Print();

			while (true)
            {
                if (!Console.KeyAvailable)
                    continue;

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.I && keyInfo.Modifiers == ConsoleModifiers.Control)
                {
                    InterfaceController.CreateController().ChangeInterface();
                    EndLoop();
                    return null;
                }

                UseKey(keyInfo);

                if (closeFlag)
                {
                    closeFlag = false;
                    ResetView();
                    return null;
                }
                if (nextView != null)
                {
                    Func<object> funcToSend = nextView;
                    nextView = null;
                    return funcToSend;
                }
            }
        }
    }
}
