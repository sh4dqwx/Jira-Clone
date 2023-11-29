using JiraClone.db.dbmodels;
using JiraClone.db.repositories;
using JiraClone.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private IProjectRepository _projectRepository;
        private IAccountRepository _accountRepository;
        private IStatusRepository _statusRepository;
        private ApplicationState _applicationState;
        private ObservableCollection<Project> _ownedProjectList;
        private ObservableCollection<Project> _sharedProjectList;

        public ProjectsViewModel(
            IProjectRepository projectRepository,
            IAccountRepository accountRepository,
            ApplicationState applicationState,
            IStatusRepository statusRepository
        ) {
            _projectRepository = projectRepository;
            _accountRepository = accountRepository;
            _statusRepository = statusRepository;
            _applicationState = applicationState;
            _ownedProjectList = new();
            _sharedProjectList = new();
        }

        public void GetOwnedProjects()
        {
            Account? loggedUser = _applicationState.GetLoggedUser();
            List<Project> projectList = _projectRepository.GetProjectsOwnedByUser(loggedUser);
            _ownedProjectList.Clear();
            foreach (Project project in projectList)
            {
                _ownedProjectList.Add(project);
            }
        }

        public void GetSharedProjects()
        {
            Account? loggedUser = _applicationState.GetLoggedUser();
            List<Project> projectList = _projectRepository.GetProjectsSharedForUser(loggedUser);
            _sharedProjectList.Clear();
            foreach (Project project in projectList)
            {
                _sharedProjectList.Add(project);
            }
        }

        public void CreateProject(string name)
        {
            Account? loggedUser = _applicationState.GetLoggedUser();
            if (loggedUser == null)
                return;

            Project newProject = new Project {
                Name = name,
                Code = name.Substring(0, 2),
                CreationTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                OwnerId = loggedUser.Id
            };

            _projectRepository.AddProject(newProject);
            _statusRepository.AddStatus(new Status { Name = "TO DO", ProjectId = newProject.Id, Order = 1 });
			_statusRepository.AddStatus(new Status { Name = "IN PROGRESS", ProjectId = newProject.Id, Order = 2 });
			_statusRepository.AddStatus(new Status { Name = "DONE", ProjectId = newProject.Id, Order = 3 });

            GetOwnedProjects();
            GetSharedProjects();
        }

        public string? RemoveProject(string name)
        {
            Account? account = _applicationState.GetLoggedUser();
            if (account == null)
                return "U¿ytkownik nie jest zalogowany";

            Project? project = _projectRepository.GetProjectByName(name);
            if (project == null)
                return "Projekt o podanej nazwie nie istnieje";

            _projectRepository.RemoveProject(project);

            GetOwnedProjects();
            GetSharedProjects();

            return null;
        }

        public string? ShareProject(string projectName, string userLogin)
        {
            Account? loggedAccount = _applicationState.GetLoggedUser();
            if (loggedAccount == null)
                return "U¿ytkownik nie jest zalogowany";

            Project? project = _projectRepository.GetProjectByName(projectName);
            if (project == null)
                return "Projekt o podanej nazwie nie istnieje";

            Account? account = _accountRepository.GetAccountByLogin(userLogin);
            if (account == null)
                return "U¿ytkownik o podanej nazwie nie istnieje";

            if (loggedAccount.Id == account.Id)
                return "Nie mo¿esz udostêpniæ projektu sobie";

            project.AssignedAccounts.Add(account);
            _projectRepository.UpdateProject(project);

            GetOwnedProjects();
            GetSharedProjects();

            return null;
        }

        public string? RevokeProject(string projectName, string userLogin)
        {
            Account? loggedAccount = _applicationState.GetLoggedUser();
            if (loggedAccount == null)
                return "U¿ytkownik nie jest zalogowany";

            Project? project = _projectRepository.GetProjectByName(projectName);
            if (project == null)
                return "Projekt o podanej nazwie nie istnieje";

            Account? account = _accountRepository.GetAccountByLogin(userLogin);
            if (account == null)
                return "U¿ytkownik o podanej nazwie nie istnieje";

            if (loggedAccount.Id == account.Id)
                return "Nie mo¿esz odebraæ dostêpu do projektu sobie";

            project.AssignedAccounts.Remove(account);
            _projectRepository.UpdateProject(project);

            GetOwnedProjects();
            GetSharedProjects();
            return null;
        }

        public ObservableCollection<Project> OwnedProjectList
        {
            get => _ownedProjectList;
        }

        public ObservableCollection<Project> SharedProjectList
        {
            get => _sharedProjectList;
        }
    }
}