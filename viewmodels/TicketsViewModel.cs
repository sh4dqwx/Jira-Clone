using JiraClone.db.dbmodels;
using JiraClone.db.repositories;
using JiraClone.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JiraClone.viewmodels
{
	public class TicketsViewModel
	{
		private ITicketRepository _ticketRepository;
		private IStatusRepository _statusRepository;
		private IAccountRepository _accountRepository;
		private ApplicationState _applicationState;
		private ObservableCollection<KeyValuePair<string, List<Ticket>>> _statusList;
		private ObservableCollection<string> _autoCompleteAccounts;

		public TicketsViewModel(
			ITicketRepository ticketRepository,
			IStatusRepository statusRepository,
			IAccountRepository accountRepository,
			ApplicationState applicationState
		) {
			_ticketRepository = ticketRepository;
			_statusRepository = statusRepository;
			_accountRepository = accountRepository;
			_applicationState = applicationState;
			_statusList = new();
			_autoCompleteAccounts = new();
		}

		public void GetAccountsToShare()
		{
			_autoCompleteAccounts.Clear();
			_autoCompleteAccounts.Add(Project.Owner.Login);
			foreach (string accountName in Project.AssignedAccounts.Select(account => account.Login))
			{
				_autoCompleteAccounts.Add(accountName);
			}
		}

		public void GetStatuses()
		{
			List<Status> statusList = _statusRepository.GetStatusesFromProject(Project);
			_statusList.Clear();
			foreach (Status status in statusList)
			{
				List<Ticket> ticketList = _ticketRepository.GetTicketsFromProject(Project, status);
				_statusList.Add(new KeyValuePair<string, List<Ticket>>(status.Name, ticketList));
			}
		}

		public string AddTicket(string title, string? description, string type)
		{
			Account? loggedUser = _applicationState.GetLoggedUser();
			if (loggedUser == null)
				return "Użytkownik nie jest zalogowany";

			List<Status> statusList = _statusRepository.GetStatusesFromProject(Project);

			Ticket? ticket = _ticketRepository.GetLatestTicketFromProject(Project);
			string ticketCode;
			if (ticket != null)
				ticketCode = Project.Code + "-" + (int.Parse(ticket.Code.Split('-', 2)[1]) + 1).ToString();
			else
				ticketCode = Project.Code + "-1";

			_ticketRepository.AddTicket(new Ticket
			{
				Title = title,
				Description = description,
				Type = type,
				Code = ticketCode,
				CreationTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
				ProjectId = Project.Id,
				ReporterId = loggedUser.Id,
				StatusId = statusList.First().Id
			});

			GetStatuses();

			return null;
		}

		public string? RemoveTicket(string code)
		{
			Account? account = _applicationState.GetLoggedUser();
			if (account == null)
				return "Użytkownik nie jest zalogowany";

			Ticket? ticket = _ticketRepository.GetTicketByCode(code, Project);
			if (ticket == null)
				return "Zadanie o podanej nazwie nie istnieje";

			_ticketRepository.RemoveTicket(ticket);
			GetStatuses();
			return null;
		}

		public string? AssignTicket(string ticketCode, string userLogin)
		{
			Account? loggedAccount = _applicationState.GetLoggedUser();
			if (loggedAccount == null)
				return "Użytkownik nie jest zalogowany";

			Ticket? ticket = _ticketRepository.GetTicketByCode(ticketCode, Project);
			if (ticket == null)
				return "Zadanie o podanej nazwie nie istnieje";

			Account? account = _accountRepository.GetAccountByLogin(userLogin);
			if (account == null)
				return "Użytkownik o podanej nazwie nie istnieje";

			if (!Project.AssignedAccounts.Contains(account))
				return "Użytkownik nie ma dostępu do tego projektu";

			ticket.AssigneeId = account.Id;
			_ticketRepository.UpdateTicket(ticket);
			GetStatuses();
			return null;
		}

		public string? UnassignTicket(string ticketCode)
		{
			Account? loggedAccount = _applicationState.GetLoggedUser();
			if (loggedAccount == null)
				return "Użytkownik nie jest zalogowany";

			Ticket? ticket = _ticketRepository.GetTicketByCode(ticketCode, Project);
			if (ticket == null)
				return "Zadanie o podanej nazwie nie istnieje";

			if (ticket.AssigneeId == null)
				return "Zadanie nie jest do nikogo przydzielone";

			ticket.AssigneeId = null;
			_ticketRepository.UpdateTicket(ticket);
			GetStatuses();
			return null;
		}

		public string? ChangeStatus(string ticketCode, string statusName)
		{
			Account? loggedAccount = _applicationState.GetLoggedUser();
			if (loggedAccount == null)
				return "Użytkownik nie jest zalogowany";

			Ticket? ticket = _ticketRepository.GetTicketByCode(ticketCode, Project);
			if (ticket == null)
				return "Zadanie o podanej nazwie nie istnieje";

			Status? status = _statusRepository.GetStatusByName(statusName);
			if (status == null)
				return "Status o podanej nazwie nie istnieje";

			Status? previousStatus = _statusRepository.GetStatusById(ticket.StatusId);
			if (previousStatus != null && previousStatus.Id == status.Id)
				return "Podane zadanie ma już ten status";

			ticket.StatusId = status.Id;
			_ticketRepository.UpdateTicket(ticket);
			GetStatuses();
			return null;
		}

		public ObservableCollection<KeyValuePair<string, List<Ticket>>> StatusList
		{
			get { return _statusList; }
		}

		public ObservableCollection<string> AutoCompleteAccounts
		{
			get => _autoCompleteAccounts;
		}

		public Project Project { get; set; }
	}
}
