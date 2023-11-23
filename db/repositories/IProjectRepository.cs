using JiraClone.db.dbmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.repositories
{
	public interface IProjectRepository
	{
		public List<Project> GetProjectsByUser(Account? account);
		public void AddProject(Project project);
		public void RemoveProject(Project project);
	}
}
