using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
