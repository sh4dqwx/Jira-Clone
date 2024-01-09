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

namespace JiraClone.graphicViews.commentsViews
{
    public partial class AddCommentDialog : Window
    {
        private CommentsViewModel _viewModel;

        public AddCommentDialog(CommentsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        private bool AreInputsValid()
        {
            bool areValid = true;
            if(Validation.GetHasError(commentTextBox)) areValid = false;
            return areValid;
        }

        private void OnSubmit(object sender, EventArgs e)
        {
            if (!AreInputsValid()) return;

            _viewModel.AddComment(commentTextBox.Text);

            Close();
        }

        public string Comment { get; set; } = string.Empty;
    }
}
