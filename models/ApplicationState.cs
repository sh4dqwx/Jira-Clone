using JiraClone.db.dbmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.models
{
    public class ApplicationState
    {
        private Account? _loggedUser;

        public bool IsUserLogged()
        {
            return _loggedUser != null;
        }

        public Account? GetLoggedUser()
        {
            return _loggedUser;
        }

        public void SetLoggedUser(Account account)
        {
            _loggedUser = account;
        }

        public void RemoveLoggedUser()
        {
            _loggedUser = null;
        }
    }
}