using JiraClone.utils;
using System.Runtime.InteropServices;
using System.Windows;

namespace JiraClone
{
    public partial class App : Application
    {
        public App() {
            MainWindow window = new();
            window.Show();
            InterfaceController.CreateController(window);
        }
    }
}
