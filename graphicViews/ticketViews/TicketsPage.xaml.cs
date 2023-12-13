using JiraClone.db.dbmodels;
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
		private TicketsViewModel _viewModel;

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

		private void OnGoBack(object sender, EventArgs e)
		{
			if (NavigationService.CanGoBack)
			{
				NavigationService.GoBack();
			}
		}

		public TicketsPage(TicketsViewModel viewModel)
		{
			InitializeComponent();
			_viewModel = viewModel;
			DataContext = _viewModel;

			_viewModel.GetStatuses();
		}
	}
}
