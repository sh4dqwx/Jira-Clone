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
            return _db.Tickets.Where(ticket => ticket.Id == id).FirstOrDefault();
        }

        public Ticket? GetTicketByCode(string code, Project project)
        {
            return _db.Tickets.Where(ticket => ticket.Code == code && ticket.ProjectId == project.Id).FirstOrDefault();
        }

        public Ticket? GetLatestTicketFromProject(Project project)
        {
            return _db.Tickets.Where(ticket => ticket.ProjectId == project.Id).OrderByDescending(ticket => ticket.CreationTimestamp).FirstOrDefault();
        }

        public List<Ticket> GetTicketsFromProject(Project project, Status status)
        {
            return _db.Tickets.Where(ticket => ticket.ProjectId == project.Id && ticket.StatusId == status.Id).ToList();
        }

        public void AddTicket(Ticket ticket)
        {
            _db.Tickets.Add(ticket);
            _db.SaveChanges();
        }

        public void UpdateTicket(Ticket ticket)
        {
            _db.Tickets.Update(ticket);
            _db.SaveChanges();
        }

        public void RemoveTicket(Ticket ticket)
        {
            _db.Tickets.Remove(ticket);
            _db.SaveChanges();
        }
    }
}