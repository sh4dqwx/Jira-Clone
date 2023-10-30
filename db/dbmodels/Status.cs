using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.dbmodels
{
	public class Status
	{
		public int Id { get; set; }
		public int IdProject { get; set; }
		public int Order { get; set; }
		public string? Name { get; set; }
		public Project? Project { get; set; }
		public List<Ticket>? Tickets { get; set; }
	}
}
