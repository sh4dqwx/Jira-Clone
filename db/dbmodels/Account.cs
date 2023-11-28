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
        public List<Project> OwnedProjects { get; set; } = new();
        public List<Project> AssignedProjects { get; set; } = new();
        public List<Ticket> ReporterTickets { get; set; } = new();
        public List<Ticket> AssigneeTickets { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }
}
