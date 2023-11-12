using JiraClone.db.dbmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.repositories
{
    public interface IAccountRepository
    {
        Account? GetAccountByLogin(string login);
        Account? GetAccountByEmail(string email);
        List<Account> GetAllAccounts();
        void AddAccount(Account account);
        void RemoveAccount(Account account);
    }
}
