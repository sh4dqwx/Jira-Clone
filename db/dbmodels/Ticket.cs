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
        public virtual Project Project { get; set; } = null!;
        public virtual Account Reporter { get; set; } = null!;
        public virtual Account? Assignee { get; set; }
        public virtual Status Status { get; set; } = null!;
        public virtual List<Comment> Comments { get; set; } = new();
	}
}
