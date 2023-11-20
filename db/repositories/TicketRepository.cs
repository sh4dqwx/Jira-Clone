using JiraClone.db.dbmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.repositories
{
    public class TicketRepository: ITicketRepository
    {
        private readonly SqliteDbContext _db;

        public TicketRepository(SqliteDbContext db)
        {
            _db = db;
        }

        public Ticket? GetTicketById(int id)
        {
            return _db.Tickets.Where(ticket  => ticket.Id == id).FirstOrDefault();
        }

        public List<Ticket> GetTicketsFromProject(Project project)
        {
            return _db.Tickets.Where(ticket =>ticket.Project.Id == project.Id).ToList();
        }

        public void AddTicket(Ticket ticket)
        {
            _db.Tickets.Add(ticket);
            _db.SaveChanges();
        }
    }
}
