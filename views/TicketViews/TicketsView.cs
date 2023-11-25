using JiraClone.db.dbmodels;
using JiraClone.utils.consoleViewParts;
using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.views.TicketViews
{
    public class TicketsView : ConsoleView
    {
        private Project? project;

        private HorizontalMenuBar statusMenu;
        private HorizontalMenu actionMenu, bottomMenu;
        private bool closeFlag = false;

        public TicketsView(
            AddTicketView addTicketView,
            RemoveTicketView removeTicketView,
            AssignTicketView assignTicketView,
            ChangeStatusView changeStatusView
        ) {
            statusMenu = new HorizontalMenuBar(1);

            actionMenu = new HorizontalMenu(2);
            actionMenu.Add(new Button("Dodaj zadanie", () => { addTicketView.Start(); ResetView(); }));
            actionMenu.Add(new Button("Usuń zadanie", () => { removeTicketView.Start(); ResetView(); }));
            actionMenu.Add(new Button("Przydziel zadanie", () => { assignTicketView.Start(); ResetView(); }));
			actionMenu.Add(new Button("Zmień status zadania", () => { changeStatusView.Start(); ResetView(); }));

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
            ResetView();

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
