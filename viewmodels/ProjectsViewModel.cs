using JiraClone.db.dbmodels;
using JiraClone.db.repositories;
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

        public ProjectsViewModel(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public List<Project> GetProjects()
        {
            //Pobieranie osoby zalogowanej
            return projectRepository.GetProjectsByUser(null);
        }

        public void CreateProject(string name)
        {
            //Pobieranie osoby zalogowanej
            Project newProject = new Project {
                Name = name,
                CreationTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Owner = null,
            };

            projectRepository.AddProject(newProject);
        }
    }
}
