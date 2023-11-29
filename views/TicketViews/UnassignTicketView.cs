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
	public class UnassignTicketView : ConsoleView
	{
		private TicketsViewModel viewModel;

		private VerticalMenu unassignTicketForm;
		private HorizontalMenu actionMenu;
		private Input ticketCodeInput;
		private Button submitButton;
		private bool closeFlag = false;

		protected override void ResetView()
		{
			Clear();

			base.ResetView();
		}

		public UnassignTicketView(TicketsViewModel ticketsViewModel)
		{
			viewModel = ticketsViewModel;

			unassignTicketForm = new VerticalMenu("ODDZIELANIE ZADANIA", 4);
			actionMenu = new HorizontalMenu(2);

			ticketCodeInput = new Input("Kod zadania", validationRule: new RequiredRule());
			submitButton = new Button("Zatwierdź", OnSubmit);

			unassignTicketForm.Add(ticketCodeInput);

			actionMenu.Add(submitButton);
			actionMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

			Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
			Add(unassignTicketForm);
			Add(actionMenu);
		}

		public void Start()
		{
			ResetView();
			Print();

			while (true)
			{
				if (!Console.KeyAvailable)
					continue;

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
			if (!ticketCodeInput.Validate()) areValid = false;
			return areValid;
		}

		private void OnSubmit()
		{
			if (!AreInputsValid()) return;

			string? error = viewModel.UnassignTicket(ticketCodeInput.Value);
			if (error != null)
			{
				submitButton.Error = error;
				Print();
			}
			else closeFlag = true;
		}
	}
}
