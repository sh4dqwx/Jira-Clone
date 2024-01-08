using JiraClone.db.dbmodels;
using JiraClone.graphicViews.commentsViews;
using JiraClone.viewmodels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

namespace JiraClone.graphicViews.ticketViews
{
	public partial class TicketsPage : Page
	{
		private CommentsPage _commentsPage;
		private TicketsViewModel _viewModel;

		private void OnSelect(object sender, EventArgs e)
		{
            if (sender is not ListBox status) return;
            if (status.DataContext is not KeyValuePair<string, List<Ticket>> tickets) return;

			//_commentsPage.SetTicket(tickets.Value[status.SelectedIndex]);
			NavigationService.Navigate(_commentsPage);
        }

		private void OnAddTicket(object sender, EventArgs e)
		{
			AddTicketDialog addTicketDialog = new(_viewModel);
			addTicketDialog.ShowDialog();
		}

		private void OnShowDetails(object sender, EventArgs e)
		{
			if (sender is not FrameworkElement icon) return;
			if (icon.DataContext is not Ticket ticket) return;

			TicketDetailsDialog ticketDetailsDialog = new(ticket);
			ticketDetailsDialog.ShowDialog();
		}

		private void OnAssignTicket(object sender, EventArgs e)
		{
			if (sender is not FrameworkElement icon) return;
			if (icon.DataContext is not Ticket ticket) return;

			AssignTicketDialog assignTicketDialog = new(_viewModel, ticket);
			assignTicketDialog.ShowDialog();
		}

		private void OnUnassignTicket(object sender, EventArgs e)
		{
			if (sender is not FrameworkElement icon) return;
			if (icon.DataContext is not Ticket ticket) return;

			string? error = _viewModel.UnassignTicket(ticket.Code);
			if (error != null)
			{
				MessageBox.Show(error, "UWAGA", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				MessageBox.Show($"Użytkownik {ticket?.Assignee?.Name} {ticket?.Assignee?.Surname} został oddzielony od zadania");
			}
		}

		private void OnChangeStatus(object sender, EventArgs e)
		{
            if (sender is not FrameworkElement icon) return;
            if (icon.DataContext is not Ticket ticket) return;

            ChangeStatusDialog changeStatusDialog = new(_viewModel, ticket);
            changeStatusDialog.ShowDialog();
        }

		private void OnRemoveTicket(object sender, EventArgs e)
		{
			if (sender is not FrameworkElement icon) return;
			if (icon.DataContext is not Ticket ticket) return;

			string? error = _viewModel.UnassignTicket(ticket.Code);
			if (error != null)
			{
				MessageBox.Show(error, "UWAGA", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void OnGoBack(object sender, EventArgs e)
		{
			if (NavigationService.CanGoBack)
			{
				NavigationService.GoBack();
			}
		}

		public TicketsPage(CommentsPage commentsPage, TicketsViewModel viewModel)
		{
			InitializeComponent();
			_commentsPage = commentsPage;
			_viewModel = viewModel;
			DataContext = _viewModel;

			_viewModel.GetStatuses();
		}
	}
}
