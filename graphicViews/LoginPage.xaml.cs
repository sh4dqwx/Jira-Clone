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

namespace JiraClone.graphicViews
{
	public partial class LoginPage : Page
	{
		//private ProjectsPage _projectsPage;
		private LoginViewModel _viewModel;

		private bool AreInputsValid()
		{
			bool areValid = true;
			if(Validation.GetHasError(loginTextBox)) areValid = false;
			if(Validation.GetHasError(passwordTextBox)) areValid = false;
			return areValid;
		}

		private void OnLogin(object sender, RoutedEventArgs e)
		{
			if (!AreInputsValid()) return;

			string? error = _viewModel.AuthenticateUser(
				login: loginTextBox.Text,
				password: passwordTextBox.Password
			);

			if (error != null)
			{
				formError.Content = error;
			}
			else
			{
				//NavigationService.Navigate(_projectPage);
			}
		}

		private void OnGoBack(object sender, RoutedEventArgs e)
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
			if (e.Content is not LoginPage) return;
			loginTextBox.Text = string.Empty;
			passwordTextBox.Password = string.Empty;
			formError.Content = string.Empty;
		}

		public LoginPage(/*ProjectsPage projectsPage,*/ LoginViewModel viewModel)
		{
			InitializeComponent();
			//_projectsPage = projectsPage;
			_viewModel = viewModel;
			Loaded += OnLoaded;
		}

		public string Login { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}
}
