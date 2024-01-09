using JiraClone.db.dbmodels;
using JiraClone.graphicViews.commentsViews;
using JiraClone.graphicViews.ticketViews;
using JiraClone.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JiraClone.graphicViews.projectsViews
{
    public partial class ProjectsPage : Page
    {
        private ProjectsViewModel _viewModel;
        private TicketsPage _ticketPage;
        public ProjectsPage(ProjectsViewModel viewModel, TicketsPage ticketsPage)
        {
            InitializeComponent();
			_viewModel = viewModel;
			_ticketPage = ticketsPage;
            DataContext = _viewModel;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            _viewModel.GetOwnedProjects();
            _viewModel.GetSharedProjects();
        }

        private void OnSelect(object sender, EventArgs e)
        {
            if (sender is not ListBox projectList) return;
            if (projectList.SelectedIndex < 0) return;

            _ticketPage.SetProject(_viewModel.OwnedProjectList[projectList.SelectedIndex]);
            NavigationService.Navigate(_ticketPage);
        }

        private void OnAddProject(object sender, EventArgs e)
        {
            AddProjectDialog addProjectDialog = new(_viewModel);
            addProjectDialog.ShowDialog();
        }

        private void OnShowDetails(object sender, EventArgs e)
        {
            if (sender is not FrameworkElement icon) return;
            if (icon.DataContext is not Project project) return;

            ProjectDetailsDialog projectDetailsDialog = new(project);
            projectDetailsDialog.ShowDialog();
        }

        private void OnAssignProject(object sender, EventArgs e)
        {
            if (sender is not FrameworkElement icon) return;
            if (icon.DataContext is not Project project) return;

            ShareProjectDialog shareProjectDialog = new(_viewModel);
            shareProjectDialog.ShowDialog();
        }

        private void OnUnassignProject(object sender, EventArgs e)
        {
            if (sender is not FrameworkElement icon) return;
            if (icon.DataContext is not Project project) return;

            RevokeProjectDialog revokeProjectDialog = new(_viewModel);
            revokeProjectDialog.ShowDialog();
        }

        private void OnRemoveProject(object sender, EventArgs e)
        {
            if (sender is not FrameworkElement icon) return;
            if (icon.DataContext is not Project project) return;

            string? error = _viewModel.RemoveProject(project.Name);
            if (error != null)
            {
                MessageBox.Show(error, "UWAGA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnGoBack(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        
    }
}
