using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils
{
    public class InterfaceController
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        private readonly MainWindow window;

        private static InterfaceController? controller;

        public static InterfaceController CreateController(MainWindow? window = null)
        {
            if (controller == null)
            {
                if (window == null)
                    throw new Exception("No window was provided to controller");
                else
                    controller = new InterfaceController(window);
            }
            return controller;
        }

        private InterfaceController(MainWindow window)
        {
            this.window = window;
            if(!window.IsVisible) 
                ShowConsole();
        }

        public void ChangeInterface()
        {
            if(window.IsVisible)
            {
                window.Hide();
                ShowConsole();
            }
            else
            {
                HideConsole();
                window.Show();
            }
        }

        private static void ShowConsole()
        {
            var handle = GetConsoleWindow();
            if (handle == IntPtr.Zero)
                AllocConsole();
            else
            {
                ShowWindow(handle, SW_SHOW);
                SetForegroundWindow(handle);
            }
        }

        private static void HideConsole()
        {
            var handle = GetConsoleWindow();
            if (handle != IntPtr.Zero)
                ShowWindow(handle, SW_HIDE);
        }
    }
}
