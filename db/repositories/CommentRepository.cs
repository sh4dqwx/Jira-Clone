using JiraClone.db.dbmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.repositories
{
    public class CommentRepository: ICommentRepository
    {
        private readonly SqliteDbContext _db;

        public CommentRepository(SqliteDbContext db)
        {
            _db = db;
        }

        public Comment? GetCommentById(int id)
        {
            return _db.Comments.Where(comment => comment.Id == id).FirstOrDefault();
        }

        public List<Comment> GetCommentsFromTicket(Ticket ticket)
        {
            return _db.Comments.Where(comment => comment.Ticket.Id == ticket.Id).ToList();
        }

        public void AddComment(Comment comment)
        {
            _db.Comments.Add(comment);
            _db.SaveChanges();
        }

        public void UpdateComment(Comment comment)
        {
            _db.Comments.Update(comment);
            _db.SaveChanges();
        }

        public void RemoveComment(Comment comment)
        {
            _db.Comments.Remove(comment);
            _db.SaveChanges();
        }
    }
}