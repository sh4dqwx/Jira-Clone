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
    public class AssignTicketView : ConsoleView
    {
		private TicketsViewModel viewModel;

		private VerticalMenu assignTicketForm;
		private HorizontalMenu actionMenu;
		private Input ticketCodeInput, userLoginInput;
		private Button submitButton;
		private bool closeFlag = false;

		protected override void ResetView()
		{
			Clear();

			base.ResetView();
		}

		public AssignTicketView(TicketsViewModel ticketsViewModel)
		{
			viewModel = ticketsViewModel;

			assignTicketForm = new VerticalMenu("PRZYDZIELANIE ZADANIA", 4);
			actionMenu = new HorizontalMenu(2);

			ticketCodeInput = new Input("Kod zadania", validationRule: new RequiredRule());
			userLoginInput = new Input("Login użytkownika", validationRule: new RequiredRule());
			submitButton = new Button("Zatwierdź", OnSubmit);

			assignTicketForm.Add(ticketCodeInput);
			assignTicketForm.Add(userLoginInput);

			actionMenu.Add(submitButton);
			actionMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

			Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
			Add(assignTicketForm);
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
			if (!userLoginInput.Validate()) areValid = false;
			return areValid;
		}

		private void OnSubmit()
		{
			if (!AreInputsValid()) return;

			string? error = viewModel.AssignTicket(ticketCodeInput.Value, userLoginInput.Value);
			if (error != null)
			{
				submitButton.Error = error;
				Print();
			}
			else closeFlag = true;
		}
	}
}
