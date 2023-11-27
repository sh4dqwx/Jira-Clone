using JiraClone.db.dbmodels;
using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.viewmodels;
using JiraClone.views.TicketViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace JiraClone.views.ProjectViews
{
    public class ProjectsView : ConsoleView
    {
        private ProjectsViewModel viewModel;

        private TicketsView ticketsView;
        private VerticalMenu projectsMenu;
        private HorizontalMenu actionMenu, bottomMenu;
        private bool closeFlag = false;

        protected override void ResetView()
        {
            projectsMenu.Clear();
            List<Project> ownedProjects = viewModel.GetOwnedProjects();
            foreach (var project in ownedProjects)
                projectsMenu.Add(new Button(project.Name, () => OnProjectClick(project)));

            List<Project> sharedProjects = viewModel.GetSharedProjects();
            foreach (var project in sharedProjects)
                projectsMenu.Add(new Button("☁ " + project.Name, () => OnProjectClick(project)));

            base.ResetView();
        }

        public ProjectsView(
            ProjectsViewModel viewModel,
            AddProjectView addProjectView,
            RemoveProjectView deleteProjectView,
            ShareProjectView shareProjectView,
            TicketsView ticketsView
        ) {
            this.ticketsView = ticketsView;

            this.viewModel = viewModel;
            viewModel.PropertyChanged += EventHandler;

            projectsMenu = new VerticalMenu("PROJEKTY", 3);

            List<Project> ownedProjects = viewModel.GetOwnedProjects();
            foreach (var project in ownedProjects)
                projectsMenu.Add(new Button(project.Name, () => OnProjectClick(project)));

            List<Project> sharedProjects = viewModel.GetSharedProjects();
            foreach (var project in sharedProjects)
                projectsMenu.Add(new Button("☁ " + project.Name, () => OnProjectClick(project)));

            actionMenu = new HorizontalMenu(2);
            actionMenu.Add(new Button("Stwórz projekt", () => { addProjectView.Start(); ResetView(); Print(); }));
            actionMenu.Add(new Button("Usuń projekt", () => { deleteProjectView.Start(); ResetView(); Print(); }));
            actionMenu.Add(new Button("Udostępnij projekt", () => { shareProjectView.Start(); ResetView(); Print(); }));

            bottomMenu = new HorizontalMenu(1);
            bottomMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

            Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
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
			Print();

			while (true)
            {
                if (!Console.KeyAvailable)
                    continue;

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                UseKey(keyInfo);

                if (closeFlag)
                {
                    closeFlag = false;
                    ResetView();
                    return;
                }
            }
        }

        private void OnProjectClick(Project project)
        {
            ticketsView.Start(project);
            ResetView();
			Print();
		}
    }
}
