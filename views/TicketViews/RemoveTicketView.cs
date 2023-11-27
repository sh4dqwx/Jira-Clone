using JiraClone.utils.consoleViewParts.layouts;
using JiraClone.utils.consoleViewParts.options;
using JiraClone.utils.validators;
using JiraClone.viewmodels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.views.TicketViews
{
    public class RemoveTicketView : ConsoleView
    {
		private TicketsViewModel viewModel;

		private VerticalMenu removeTicketForm;
		private HorizontalMenu actionMenu;
		private Input codeInput;
		private Button submitButton;
		private bool closeFlag = false;

		protected override void ResetView()
		{
			codeInput.Clear();

			base.ResetView();
		}

		public RemoveTicketView(TicketsViewModel ticketsViewModel)
		{
			viewModel = ticketsViewModel;

			removeTicketForm = new VerticalMenu("USUWANIE ZADANIA", 3);
			actionMenu = new HorizontalMenu(2);

			codeInput = new Input("Kod", validationRule: new RequiredRule());
			submitButton = new Button("Zatwierdź", OnSubmit);

			removeTicketForm.Add(codeInput);

			actionMenu.Add(submitButton);
			actionMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

			Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
			Add(removeTicketForm);
			Add(actionMenu);
		}

		public void Start()
		{
			ResetView();
			Print();

			while (true)
			{
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

		private bool AreInputsValid()
		{
			bool areValid = true;
			if (!codeInput.Validate()) areValid = false;
			return areValid;
		}

		private void OnSubmit()
		{
			if (!AreInputsValid()) return;

			string? error = viewModel.RemoveTicket(codeInput.Value);

			if (error != null)
			{
				submitButton.Error = error;
				Print();
			}
			else closeFlag = true;
		}
	}
}
