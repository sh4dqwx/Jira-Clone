using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
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

		[DllImport("kernel32.dll")]
		private static extern IntPtr GetConsoleScreenBufferInfo();

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            uint lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            uint hTemplateFile
        );

		[DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

		private const uint GENERIC_WRITE = 0x40000000;
		private const uint FILE_SHARE_WRITE = 0x2;
		private const uint OPEN_EXISTING = 0x3;

        private const int MF_BYCOMMAND = 0x00000000;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_SIZE = 0xF000;

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
            { 
                AllocConsole();


                DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_SIZE, MF_BYCOMMAND);


                IntPtr stdHandle = CreateFile("CONOUT$", GENERIC_WRITE, FILE_SHARE_WRITE, 0, OPEN_EXISTING, 0, 0);
				StreamWriter standardOutput = new StreamWriter(
                    new FileStream(
                        new SafeFileHandle(stdHandle, true),                        
                        FileAccess.Write
                    )
                )
                { 
                    AutoFlush = true
                };
				Console.SetOut(standardOutput);
                Console.Clear();
                Console.SetWindowSize(120, 35);
                Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
			}

			ShowWindow(handle, SW_SHOW);
			SetForegroundWindow(handle);
        }

        private static void HideConsole()
        {
            var handle = GetConsoleWindow();
            if (handle != IntPtr.Zero)
                ShowWindow(handle, SW_HIDE);
        }
    }
}
