using System;
using System.ComponentModel;

namespace JiraClone.viewmodels
{
    public class LoginViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public void KeyPressed(ConsoleKeyInfo key)
		{
			Console.Write(key.KeyChar.ToString());
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(GetType().Name));
		}
	}
}
