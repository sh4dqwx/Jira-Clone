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
        public ProjectsPage(ProjectsViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
        }

        private void OnAddProject(object sender, EventArgs e)
        {
            AddProjectDialog addProjectDialog = new(_viewModel);
            addProjectDialog.ShowDialog();
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
