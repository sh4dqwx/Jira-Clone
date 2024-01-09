using JiraClone.graphicViews.projectsViews;
using JiraClone.viewmodels;
using JiraClone.views.ProjectViews;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

namespace JiraClone.graphicViews
{
    public partial class RegisterPage : Page
    {
        private ProjectsPage _projectsPage;
        private RegisterViewModel _viewModel;

        private bool AreInputsValid()
        {
            bool areValid = true;
            if (Validation.GetHasError(loginTextBox)) areValid = false;
			if (Validation.GetHasError(passwordTextBox)) areValid = false;
			if (Validation.GetHasError(emailTextBox)) areValid = false;
			if (Validation.GetHasError(nameTextBox)) areValid = false;
			if (Validation.GetHasError(surnameTextBox)) areValid = false;
            return areValid;
		}

        private void OnRegister(object sender, EventArgs e)
        {
			if (!AreInputsValid()) return;

			string? error = _viewModel.RegisterUser(
				login: loginTextBox.Text,
				password: passwordTextBox.Password,
				email: emailTextBox.Text,
				name: nameTextBox.Text,
				surname: surnameTextBox.Text
            );

			if (error != null)
			{
				formError.Content = error;
			}
			else
            {
                NavigationService.Navigate(_projectsPage);
            }
		}

        private void OnGoBack(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }

        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigated += OnNavigated;
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is not RegisterPage) return;
            loginTextBox.Text = string.Empty;
            passwordTextBox.Password = string.Empty;
            emailTextBox.Text = string.Empty;
            nameTextBox.Text = string.Empty;
            surnameTextBox.Text = string.Empty;
            formError.Content = string.Empty;
        }

        public RegisterPage(ProjectsPage projectsPage, RegisterViewModel viewModel)
        {
            InitializeComponent();
            _projectsPage = projectsPage;
            _viewModel = viewModel;
            Loaded += OnLoaded;
        }

        public string Login { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
	}
}
