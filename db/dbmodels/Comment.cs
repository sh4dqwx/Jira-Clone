using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.dbmodels
{
	public class Comment
	{
		public int Id { get; set; }
		public int IdTicket { get; set; }
		public int IdAccount { get; set; }
		public string? Content { get; set; }
		public long CreationTimestamp { get; set; }
		public Ticket? Ticket { get; set; }
		public Account? Account { get; set; }
	}
}
