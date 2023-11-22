using JiraClone.db.dbmodels;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.repositories
{
	public class ProjectRepository : IProjectRepository
	{
		private readonly SqliteDbContext _db;

		public ProjectRepository(SqliteDbContext db)
		{
			_db = db;
		}
		public List<Project> GetProjectsByUser(Account account)
		{
			return _db.Projects
				.Where(project => project.Owner.Id == account.Id || project.AssignedAccounts.Contains(account))
				.ToList();
		}

		public void AddProject(Project project)
		{
			_db.Projects.Add(project);
			_db.SaveChanges();
		}

		public void RemoveProject(Project project)
		{
			_db.Projects.Remove(project);
			_db.SaveChanges();
		}
	}
}
