using JiraClone.utils.views;
using JiraClone.viewmodels;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace JiraClone.views
{
	public class LoginView
	{
		private LoginViewModel viewModel;
		private IOption[] options = new IOption[]
		{
			new TextOption("Login", 0, 11),
			new TextOption("Hasło", 0, 12, true),
			new Option("Zaloguj się", 0, 13)
		};
		private int selectedOption;

		public LoginView()
		{
			viewModel = new();
			viewModel.PropertyChanged += EventHandler;
		}

		private void EventHandler(object sender, PropertyChangedEventArgs e)
		{
			Console.WriteLine(e.PropertyName);
		}

		public void Start()
		{
			Console.WriteLine(Logo.ConsoleLogo);
			foreach (var option in options) { Console.WriteLine(option); }
			Console.CursorVisible = false;

			options[0].Select();

			while (true)
			{
				ConsoleKeyInfo key = Console.ReadKey(true);
				switch (key.Key)
				{
					case ConsoleKey.UpArrow:
						if (selectedOption <= 0) break;
						options[selectedOption--].Unselect();
						options[selectedOption].Select();
						break;
					case ConsoleKey.DownArrow:
						if (selectedOption >= options.Length - 1) break;
						options[selectedOption++].Unselect();
						options[selectedOption].Select();
						break;
				}

				if (options[selectedOption] is not IInputOption) continue;
				IInputOption inputOption = (IInputOption)options[selectedOption];
				inputOption.Write(key.KeyChar);
			}
		}
	}
}
