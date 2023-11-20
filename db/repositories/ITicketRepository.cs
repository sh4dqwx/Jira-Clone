using JiraClone.db.dbmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.repositories
{
    public interface ITicketRepository
    {
        public Ticket? GetTicketById(int id);
        public List<Ticket> GetTicketsFromProject(Project project);
        public void AddTicket(Ticket ticket);
        public void UpdateTicket(Ticket ticket);
        public void RemoveTicket(Ticket ticket);
    }
}
