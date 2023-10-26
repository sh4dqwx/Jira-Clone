using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.dbmodels
{
    public class Account
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public long CreationTimestamp { get; set; }
        public List<AccountProject>? AccountProjects { get; set; }
        public List<Ticket>? Tickets { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
