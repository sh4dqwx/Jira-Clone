using JiraClone.db.dbmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.repositories
{
    public interface ICommentRepository
    {
        public Comment? GetCommentById(int id);
        public List<Comment> GetCommentsFromTicket(Ticket ticket);
        public void AddComment(Comment comment);
        public void UpdateComment(Comment comment);
        public void RemoveComment(Comment comment);
    }
}
