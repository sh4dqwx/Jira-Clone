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
        public Account? GetAccountByLogin(string login);
        public Account? GetAccountByEmail(string email);
        public Account? GetAccountById(int id);
        public List<Account> GetAllAccounts();
        public void AddAccount(Account account);
        public void RemoveAccount(Account account);
    }
}
