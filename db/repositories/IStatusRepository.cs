using JiraClone.db.dbmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.repositories
{
    public interface IStatusRepository
    {
        public Status? GetStatusById(int id);
        public List<Status> GetStatusesFromProject(Project project);
    }
}
