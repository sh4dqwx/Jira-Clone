using JiraClone.db.dbmodels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        private readonly SqliteDbContext _db;

        public AccountRepository(SqliteDbContext db)
        {
            this._db = db;
        }

        public Account? GetAccountByLogin(string login)
        {
            return _db.Accounts.Where(account => account.Login == login).FirstOrDefault();
        }

        public Account? GetAccountByEmail(string email)
        {
            return _db.Accounts.Where(account => account.Email == email).FirstOrDefault();
        }

        public Account? GetAccountById(int id)
        {
            return _db.Accounts.Where(account => account.Id == id).FirstOrDefault();
        }

		public List<Account> GetAllAccounts()
        {
            return _db.Accounts.ToList();
        }

        public void AddAccount(Account account)
        {
            _db.Accounts.Add(account);
            _db.SaveChanges();
        }

        public void RemoveAccount(Account account)
        {
            _db.Accounts.Remove(account);
            _db.SaveChanges();
        }
    }
}
