using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.dbmodels
{
    public record class Ticket
	{
		public int Id { get; set; }
		public int ProjectId { get; set; }
		public int ReporterId { get; set; }
		public int? AssigneeId { get; set; }
		public int StatusId { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public required string Type { get; set; }
		public required string Code { get; set; }
		public long CreationTimestamp { get; set; }
		public Project Project { get; set; } = null!;
		public Account Reporter { get; set; } = null!;
		public Account? Assignee { get; set; }
		public Status Status { get; set; } = null!;
		public List<Comment> Comments { get; set; } = new();
	}
}
