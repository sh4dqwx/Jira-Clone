using JiraClone.db.dbmodels;
using JiraClone.db.repositories;
using JiraClone.models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JiraClone.viewmodels
{
    public class LoginViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		private IAccountRepository accountRepository;
        private ApplicationState applicationState;

        public LoginViewModel(IAccountRepository accountRepository, ApplicationState applicationState)
		{
			this.accountRepository = accountRepository;
			this.applicationState = applicationState;
		}

		public string? AuthenticateUser(string login, string password)
		{
			Account? account = accountRepository.GetAccountByLogin(login);
			if (account == null)
				return "Konto nie istnieje";
			if (!password.Trim().Equals(account.Password))
				return "Błędne hasło";

			applicationState.SetLoggedUser(account);

			return null;
		}
	}
}
