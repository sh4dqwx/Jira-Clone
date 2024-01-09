using JiraClone.db.dbmodels;
using JiraClone.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JiraClone.graphicViews.commentsViews
{
    public partial class CommentsPage : Page
    {
        private CommentsViewModel _viewModel;

        public CommentsPage(CommentsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel.GetComments();
        }

        private void OnAddComment(object sender, RoutedEventArgs e)
        {
            AddCommentDialog addCommentDialog = new(_viewModel);
            addCommentDialog.ShowDialog();
        }

        private void OnGoBack(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        public void SetTicket(Ticket ticket)
        {
            _viewModel.Ticket = ticket;
        }
    }
}
