using JiraClone.db.dbmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.repositories
{
    public class StatusRepository: IStatusRepository
    {
        private readonly SqliteDbContext _db;

        public StatusRepository(SqliteDbContext db)
        {
            _db = db;
        }

        public Status? GetStatusById(int id)
        {
            return _db.Statuses.Where(status => status.Id == id).FirstOrDefault();
        }

        public List<Status> GetStatusesFromProject(Project project)
        {
            return _db.Statuses.Where(status => status.Project.Id == project.Id).ToList();
        }

        public void AddStatus(Status status)
        {
            _db.Statuses.Add(status);
            _db.SaveChanges();
        }

        public void UpdateStatus(Status status)
        {
            _db.Statuses.Update(status);
            _db.SaveChanges();
        }

        public void DeleteStatus(Status status)
        {
            _db.Statuses.Remove(status);
            _db.SaveChanges();
        }
    }
}
