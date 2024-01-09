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
using System.Windows.Shapes;

namespace JiraClone.graphicViews.ticketViews
{
    public partial class AddTicketDialog : Window
    {
        private TicketsViewModel _viewModel;

        private bool AreInputsValid()
        {
            bool areValid = true;
            if (Validation.GetHasError(titleTextBox)) areValid = false;
			if (Validation.GetHasError(descriptionTextBox)) areValid = false;
            return areValid;
		}

        private void OnSubmit(object sender, EventArgs e)
        {
            if (!AreInputsValid()) return;

			string error = _viewModel.AddTicket(
                titleTextBox.Text,
                descriptionTextBox.Text,
                (string)((ComboBoxItem)typeComboBox.SelectedItem).Content
            );

			if (error != null)
			{
				formError.Content = error;
			}
            else
            {
                Close();
            }
		}

        public AddTicketDialog(TicketsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        public string TicketTitle { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
	}
}
