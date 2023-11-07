﻿using JiraClone.utils.consoleViewParts;
using JiraClone.viewmodels;
using System;
using System.ComponentModel;
using JiraClone.utils;

namespace JiraClone.views
{
	public class LoginView
	{
		private LoginViewModel viewModel;
		private IOption[] options = new IOption[]
		{
			new TextOption("Login", 0, 12),
			new TextOption("Hasło", 0, 13, true),
			//new Option("Zaloguj się", 0, 14)
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
            //foreach (var line in Constants.ConsoleLogo)
            //    printCenter(line);

            Console.WriteLine("LOGOWANIE");
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

				options[selectedOption].UseKey(key.KeyChar);
			}
		}
	}
}
