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

		private void OnTicketsChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if(e.NewItems == null)
			{
				
				return;
			}
			foreach (KeyValuePair<string, List<Ticket>> status in e.NewItems)
			{
				
			}
		}

		public TicketsPage(TicketsViewModel viewModel)
		{
			InitializeComponent();
			_viewModel = viewModel;
			DataContext = _viewModel;

			_viewModel.StatusList.CollectionChanged += OnTicketsChanged!;
			_viewModel.GetStatuses();
		}
	}
}
