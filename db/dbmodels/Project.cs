using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.dbmodels
{
	public class Project
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public long CreationTimestamp { get; set; }
		public List<Account> Accounts { get; set; } = new();
		public List<Ticket> Tickets { get; set; } = new();
		public List<Status> Statuses { get; set; } = new();
	}
}
