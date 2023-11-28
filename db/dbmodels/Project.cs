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
		public Account Owner { get; set; } = null!;
		public List<Account> AssignedAccounts { get; set; } = new();
		public List<Ticket> Tickets { get; set; } = new();
		public List<Status> Statuses { get; set; } = new();
	}
}
