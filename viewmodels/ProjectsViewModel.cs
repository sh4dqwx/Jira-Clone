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
        private ApplicationState applicationState;

        public ProjectsViewModel(IProjectRepository projectRepository, ApplicationState applicationState)
        {
            this.projectRepository = projectRepository;
            this.applicationState = applicationState;
        }

        public List<Project> GetProjects()
        {
            Account loggedUser = applicationState.GetLoggedUser();
            return projectRepository.GetProjectsByUser(loggedUser);
        }

        public void CreateProject(string name)
        {
            Account loggedUser = applicationState.GetLoggedUser();
            Project newProject = new Project {
                Name = name,
                CreationTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Owner = loggedUser,
            };

            projectRepository.AddProject(newProject);
        }
    }
}
