using JiraClone.db.dbmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.repositories
{
    public class AccountRepository: IAccountRepository
    {
        private readonly SqliteDbContext db;

        public AccountRepository(SqliteDbContext db)
        {
            this.db = db;
        }

        public Account? GetAccountByLogin(string login)
        {
            return db.Accounts.Where(account => account.Login == login).FirstOrDefault();
        } 

        public List<Account> GetAllAccounts()
        {
            return db.Accounts.ToList();
        }

        public void AddAccount(Account account)
        {
            db.Accounts.Add(account);
        }

        public void RemoveAccount(Account account)
        {
            db.Accounts.Remove(account);
        }
    }
}
