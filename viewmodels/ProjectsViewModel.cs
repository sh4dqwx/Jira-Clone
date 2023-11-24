using JiraClone.db.dbmodels;
using JiraClone.db.repositories;
using JiraClone.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}