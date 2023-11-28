using JiraClone.db.dbmodels;
using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.views.CommentViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace JiraClone.views.TicketViews
{
    public class TicketDetailsView : ConsoleView
    {
        private TextField textField;
        private HorizontalMenu actionMenu;
        private bool closeFlag = false;
        private Ticket currentTicket;

        public TicketDetailsView(CommentsView commentsView)
        {
            textField = new TextField("", height: 15, width: 60);

            actionMenu = new HorizontalMenu(2);
            actionMenu.Add(new Button("Komentarze", () => StartNewConsoleView(() => commentsView.Start(currentTicket))));
            actionMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

            Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(new Text("SZCZEGÓŁY ZADANIA"));
            Add(textField);
            Add(actionMenu);
        }

        protected override void ResetView()
        {
            Clear();

            StringBuilder sb = new StringBuilder();
            sb.Append("Nazwa zadania: " + currentTicket.Title + '\n');
            if (currentTicket.Description == null)
                sb.Append("Opis zadania: ---\n");
            else sb.Append("Opis zadania: " + currentTicket.Description + '\n');
            sb.Append("Typ zadania: " + currentTicket.Type + '\n');
            sb.Append("Kod zadania: " + currentTicket.Code + '\n');
            sb.Append("Data stworzenia zadania: " + new DateTime(currentTicket.CreationTimestamp * 10).ToString("yyyy-MM-dd HH:mm:ss") + '\n');
            sb.Append("Nazwa twórcy zadania: " + currentTicket.Reporter.Name + ' ' + currentTicket.Reporter.Surname + '\n');
            if (currentTicket.Assignee == null)
                sb.Append("Nazwa wykonawcy zadania: ---\n");
            else sb.Append("Nazwa wykonawcy zadania: " + currentTicket.Assignee.Name + ' ' + currentTicket.Assignee.Surname + '\n');
            sb.Append("Nazwa projektu: " + currentTicket.Project.Name + '\n');
            sb.Append("Ilość komentarzy: " + currentTicket.Comments.Count() + '\n');

            textField.Value = sb.ToString();
            base.ResetView();
        }

        public void Start(Ticket ticket)
        {
            currentTicket = ticket;
            ResetView();
            Print();

            while (true)
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
