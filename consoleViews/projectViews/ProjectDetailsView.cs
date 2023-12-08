using JiraClone.db.dbmodels;
using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.viewmodels;
using JiraClone.views.TicketViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.views.ProjectViews
{
    public class ProjectDetailsView: ConsoleView
    {
        private TextField textField;
        private HorizontalMenu actionMenu;
        private bool closeFlag = false;
        private Project currentProject;

        public ProjectDetailsView(TicketsView ticketsView)
        {
            textField = new TextField("", height: 10, width: 60);

            actionMenu = new HorizontalMenu(2);
            actionMenu.Add(new Button("Zadania", () => StartNewConsoleView(() => ticketsView.Start(currentProject))));
            actionMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

            Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(new Text("SZCZEGÓŁY PROJEKTU"));
            Add(textField);
            Add(actionMenu);
        }

        protected override void ResetView()
        {
            Clear();

            StringBuilder sb = new StringBuilder();
            sb.Append("Nazwa projektu: " + currentProject.Name + '\n');
            sb.Append("Data stworzenia projektu: " + DateTimeOffset.FromUnixTimeSeconds(currentProject.CreationTimestamp).ToString("yyyy-MM-dd HH:mm:ss") + '\n');
            sb.Append("Nazwa właściciela projektu: " + currentProject.Owner.Name + ' ' + currentProject.Owner.Surname + '\n');
            sb.Append("Ilość osób z dostępem: " + currentProject.AssignedAccounts.Count + '\n');
            sb.Append("Ilość zgłoszeń: " + currentProject.Tickets.Count + '\n');

            textField.Value = sb.ToString();
            base.ResetView();
        }

        public void Start(Project project)
        {
            currentProject = project;
            ResetView();
            Print();

            while(true)
            {
                if (!Console.KeyAvailable)
                    continue;

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
