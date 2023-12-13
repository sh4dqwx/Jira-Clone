using JiraClone.db.dbmodels;
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
using System.Windows.Shapes;

namespace JiraClone.graphicViews.ticketViews
{
    /// <summary>
    /// Interaction logic for TicketDetailsDialog.xaml
    /// </summary>
    public partial class TicketDetailsDialog : Window
    {
        public TicketDetailsDialog(Ticket ticket)
        {
            InitializeComponent();
            DataContext = ticket;
        }
    }
}
