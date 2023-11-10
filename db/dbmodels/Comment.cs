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
		public int TicketId { get; set; }
		public int AccountId { get; set; }
		public required string Content { get; set; }
		public long CreationTimestamp { get; set; }
		public Ticket Ticket { get; set; } = null!;
		public Account Account { get; set; } = null!;
	}
}
