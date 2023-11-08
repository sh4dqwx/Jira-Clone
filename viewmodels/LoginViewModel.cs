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

		public string? AuthenticateUser(string login, string password)
		{
			Account? account = accountRepository.GetAccountByLogin(login);
			if (account == null)
				return "Konto nie istnieje";
			if (!password.Trim().Equals(account.Password))
				return "Błędne hasło";

			return null;
		}
	}
}
