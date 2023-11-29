using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.dbmodels
{
    public record class Project
	{
		public int Id { get; set; }
		public int OwnerId { get; set; }
		public required string Name { get; set; }
		public long CreationTimestamp { get; set; }
		public required string Code { get; set; }
        public virtual Account Owner { get; set; } = null!;
        public virtual List<Account> AssignedAccounts { get; set; } = new();
        public virtual List<Ticket> Tickets { get; set; } = new();
        public virtual List<Status> Statuses { get; set; } = new();
	}
}
