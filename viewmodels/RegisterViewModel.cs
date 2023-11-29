using JiraClone.db.dbmodels;
using JiraClone.db.repositories;
using JiraClone.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.viewmodels
{
    public class RegisterViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private IAccountRepository accountRepository;
        private ApplicationState applicationState;

        public RegisterViewModel(IAccountRepository accountRepository, ApplicationState applicationState)
        {
            this.accountRepository = accountRepository;
            this.applicationState = applicationState;
        }

        public string? RegisterUser(string login, string password, string email, string name, string surname)
        {
            Account? accountByLogin = accountRepository.GetAccountByLogin(login);
            if (accountByLogin != null)
                return "Konto o podanym loginie już istnieje";

            Account? accountByEmail = accountRepository.GetAccountByEmail(email);
            if (accountByEmail != null)
                return "Konto o podanym email'u już istnieje";

            Account newAccount = new Account
            {
                Login = login,
                Password = password,
                Email = email,
                Name = name,
                Surname = surname,
                CreationTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            };
            accountRepository.AddAccount(newAccount);

            Account? createdAccount = accountRepository.GetAccountByLogin(login);

            applicationState.SetLoggedUser(createdAccount);

            return null;
        }
    }
}
