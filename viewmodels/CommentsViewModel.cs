using JiraClone.db.dbmodels;
using JiraClone.db.repositories;
using JiraClone.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.viewmodels
{
    public class CommentsViewModel
    {
        private ICommentRepository _commentRepository;
        private ApplicationState _applicationState;
        private ObservableCollection<Comment> _commentList;

        public CommentsViewModel(
            ICommentRepository commentRepository,
            ApplicationState applicationState
        ) {
            _commentRepository = commentRepository;
            _applicationState = applicationState;
            _commentList = new();
        }

        public void GetComments()
        {
            List<Comment> commentList = _commentRepository.GetCommentsFromTicket(Ticket);
            _commentList.Clear();
            foreach (Comment comment in commentList)
            {
                _commentList.Add(comment);
            }
        }

        public string AddComment(string content)
        {
            Account? loggedUser = _applicationState.GetLoggedUser();
            if (loggedUser == null)
                return "Użytkownik nie jest zalogowany";

            _commentRepository.AddComment(new Comment
            {
                Content = content,
                CreationTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                TicketId = Ticket.Id,
                AccountId = loggedUser.Id
            });

            GetComments();

            return null;
        }

        public ObservableCollection<Comment> CommentList
        {
            get { return _commentList; }
        }

        public Ticket Ticket { get; set; }
    }
}
