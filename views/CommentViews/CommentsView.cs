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

namespace JiraClone.views.CommentViews
{
    public class CommentsView: ConsoleView
    {
        private CommentsViewModel viewModel;

        private VerticalMenu commentsMenu;
        private HorizontalMenu actionMenu;
        private Ticket currentTicket;
        private bool closeFlag = false;

        protected override void ResetView()
        {
            commentsMenu.Clear();
            //Pobieranie i dodawanie przycisków
            base.ResetView();
        }

        private void OnCommentsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null)
            {
                commentsMenu.ClearChildren();
                return;
            }
            foreach (Comment comment in e.NewItems)
            {
                commentsMenu.Add(new Button(
                    $"{comment.Account.Name} {comment.Account.Surname}: {comment.Content}",
                    () => { }
                ));
            }
        }

        public CommentsView(CommentsViewModel commentsViewModel/*, AddComentView addCommentView*/)
        {
            viewModel = commentsViewModel;
            viewModel.CommentList.CollectionChanged += OnCommentsChanged!;

            commentsMenu = new VerticalMenu("KOMENTARZE", 3);

            actionMenu = new HorizontalMenu(2);
            actionMenu.Add(new Button("Dodaj komentarz", () => { /*StartNewConsoleView(addCommentView(ticket))*/ }));
            actionMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

            Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(commentsMenu);
            Add(actionMenu);
        }

        public void Start(Ticket ticket)
        {
            currentTicket = ticket;

            viewModel.Ticket = ticket;
            viewModel.GetComments();
            ResetView();
            Print();

            while (true)
            {
                if (!Console.KeyAvailable)
                    continue;

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                UseKey(keyInfo);

                if(closeFlag)
                {
                    closeFlag = false;
                    ResetView();
                    return;
                }
            }
        }
    }
}
