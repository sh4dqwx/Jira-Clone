using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.dbmodels
{
    public record class Status
	{
		public int Id { get; set; }
		public int ProjectId { get; set; }
		public required string Name { get; set; }
		public int Order { get; set; }
		public Project Project { get; set; } = null!;
		public List<Ticket> Tickets { get; set; } = new();
	}
}
