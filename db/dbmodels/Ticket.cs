using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.dbmodels
{
	public class Ticket
	{
		public int Id { get; set; }
		public int IdProject { get; set; }
		public int IdReporter { get; set; }
		public int IdAsignee { get; set; }
		public int IdStatus { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? Type { get; set; }
		public string? Code { get; set; }
		public long CreationTimestamp { get; set; }
		public Project? Project { get; set; }
		public Account? Reporter { get; set; }
		public Account? Asignee { get; set; }
		public Status? Status { get; set; }
		public List<Comment>? Comments { get; set; }
	}
}
