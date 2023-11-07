using JiraClone.db.dbmodels;
using JiraClone.db.repositories;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JiraClone.viewmodels
{
    public class LoginViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		private IAccountRepository accountRepository;

        public LoginViewModel(IAccountRepository accountRepository)
		{
			this.accountRepository = accountRepository;
		}

		public bool AuthenticateUser(string login, string password)
		{
			Account? account = accountRepository.GetAccountByLogin(login);
			if (account == null)
				throw new Exception("Konto nie istnieje");
			if (!account.Password.Equals(password.Trim()))
				throw new Exception("Błędne hasło");

			return true;
		}
	}
}
