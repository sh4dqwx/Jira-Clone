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
    public partial class WelcomePage : Page
    {
        private LoginPage _loginPage;
        private RegisterPage _registerPage;

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(_loginPage);
        }

        private void OnRegister(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(_registerPage);
        }

        public WelcomePage(LoginPage loginPage, RegisterPage registerPage)
        {
            _loginPage = loginPage;
            _registerPage = registerPage;
            InitializeComponent();
        }
    }
}
