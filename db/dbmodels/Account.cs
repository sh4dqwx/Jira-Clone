using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.dbmodels
{
    public record class Account
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public long CreationTimestamp { get; set; }
        public virtual List<Project> OwnedProjects { get; set; } = new();
        public virtual List<Project> AssignedProjects { get; set; } = new();
        public virtual List<Ticket> ReporterTickets { get; set; } = new();
        public virtual List<Ticket> AssigneeTickets { get; set; } = new();
        public virtual List<Comment> Comments { get; set; } = new();
    }
}
