using JiraClone.db.dbmodels;
using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.viewmodels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.views.TicketViews
{
    public class TicketsView : ConsoleView
    {
        private TicketsViewModel viewModel;
        private Project? project;

        private HorizontalMenuBar statusMenu;
        private HorizontalMenu actionMenu, bottomMenu;
        private bool closeFlag = false;

        private void OnTicketsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null)
            {
				statusMenu.Clear();
                return;
			}
			foreach (KeyValuePair<string, List<Ticket>> status in e.NewItems)
			{
				VerticalMenu vMenu = new VerticalMenu(status.Key, 3);
				foreach (Ticket ticket in status.Value)
				{
					vMenu.Add(new Button($"[{ticket.Code}] {ticket.Title}", () => { }));
				}
				statusMenu.Add(vMenu);
			}
		}

        public TicketsView(
            TicketsViewModel ticketsViewModel,
            AddTicketView addTicketView,
            RemoveTicketView removeTicketView,
            AssignTicketView assignTicketView,
            ChangeStatusView changeStatusView
        ) {
            viewModel = ticketsViewModel;
            viewModel.StatusList.CollectionChanged += OnTicketsChanged!;

            statusMenu = new HorizontalMenuBar(1);

            actionMenu = new HorizontalMenu(2);
            actionMenu.Add(new Button("Dodaj zadanie", () => { StartNewConsoleView(addTicketView.Start); }));
            actionMenu.Add(new Button("Usuń zadanie", () => { StartNewConsoleView(removeTicketView.Start); }));
            actionMenu.Add(new Button("Przydziel zadanie", () => { StartNewConsoleView(assignTicketView.Start); }));
			actionMenu.Add(new Button("Zmień status zadania", () => { StartNewConsoleView(changeStatusView.Start); }));

			bottomMenu = new HorizontalMenu(1);
			bottomMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

			Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(new Text("ZADANIA"));
            Add(statusMenu);
            Add(actionMenu);
            Add(bottomMenu);
        }

        public void Start(Project project)
        {
            this.project = project;
            viewModel.Project = project;
            viewModel.GetStatuses();
			ResetView();
            Print();
			
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                UseKey(keyInfo);

				if (closeFlag)
				{
					closeFlag = false;
					ResetView();
					return;
				}
			}
        }
    }
}
