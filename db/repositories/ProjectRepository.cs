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
		private readonly IAccountRepository _accountRepository;
		private readonly SqliteDbContext _db;

		public ProjectRepository(IAccountRepository accountRepository, SqliteDbContext db)
		{
			_accountRepository = accountRepository;
			_db = db;
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

		public List<Project> GetAllProjects(Account account)
		{
			return _db.Projects
				.Where(project => project.Owner == account || project.AssignedAccounts.Contains(account))
				.ToList();
		}
	}
}
