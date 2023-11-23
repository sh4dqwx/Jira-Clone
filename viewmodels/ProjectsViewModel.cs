using JiraClone.db.dbmodels;
using JiraClone.db.repositories;
using JiraClone.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JiraClone.viewmodels
{
    public class ProjectsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private IProjectRepository projectRepository;
        private IAccountRepository accountRepository;
        private ApplicationState applicationState;

        public ProjectsViewModel(IProjectRepository projectRepository, IAccountRepository accountRepository, ApplicationState applicationState)
        {
            this.projectRepository = projectRepository;
            this.accountRepository = accountRepository;
            this.applicationState = applicationState;
        }

        public List<Project> GetProjects()
        {
            Account? loggedUser = applicationState.GetLoggedUser();
            return projectRepository.GetProjectsByUser(loggedUser);
        }

        public void CreateProject(string name)
        {
            Account? loggedUser = applicationState.GetLoggedUser();
            if (loggedUser == null)
                return;

            Project newProject = new Project {
                Name = name,
                CreationTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                OwnerId = loggedUser.Id
            };

            projectRepository.AddProject(newProject);
        }

        public string? RemoveProject(string name)
        {
            Account? account = applicationState.GetLoggedUser();
            if (account == null)
                return "U¿ytkownik nie jest zalogowany";

            Project? project = projectRepository.GetProjectByName(name);
            if (project == null)
                return "Projekt o podanej nazwie nie istnieje";

            projectRepository.RemoveProject(project);
            return null;
        }

        public string? ShareProject(string projectName, string userLogin)
        {
            Account? loggedAccount = applicationState.GetLoggedUser();
            if (loggedAccount == null)
                return "U¿ytkownik nie jest zalogowany";

            Project? project = projectRepository.GetProjectByName(projectName);
            if (project == null)
                return "Projekt o podanej nazwie nie istnieje";

            Account? account = accountRepository.GetAccountByLogin(userLogin);
            if (account == null)
                return "U¿utkownik o podanej nazwie nie istnieje";

            project.AssignedAccounts.Add(account);
            projectRepository.UpdateProject(project);

            return null;
        }
    }
}