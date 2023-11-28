using JiraClone.db.dbmodels;
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
    public class AddTicketView : ConsoleView
    {
		private Project? _project;
		private TicketsViewModel viewModel;

		private VerticalMenu addTicketForm;
		private HorizontalMenu actionMenu;
		private Input titleInput, descriptionInput, typeInput;
		private Button submitButton;
		private bool closeFlag = false;

		protected override void ResetView()
		{
			Clear();

			base.ResetView();
		}

		public AddTicketView(TicketsViewModel ticketsViewModel)
		{
			viewModel = ticketsViewModel;

			addTicketForm = new VerticalMenu("TWORZENIE ZADANIA", 3);
			actionMenu = new HorizontalMenu(2);

			titleInput = new Input("Tytuł", validationRule: new RequiredRule());
			descriptionInput = new Input("Opis");
			typeInput = new Input("Typ", validationRule: new TicketTypeRule());
			submitButton = new Button("Zatwierdź", OnSubmit);

			addTicketForm.Add(titleInput);
			addTicketForm.Add(descriptionInput);
			addTicketForm.Add(typeInput);

			actionMenu.Add(submitButton);
			actionMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

			Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
			Add(addTicketForm);
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
			if (!titleInput.Validate()) areValid = false;
			if (!descriptionInput.Validate()) areValid = false;
			if (!typeInput.Validate()) areValid = false;
			return areValid;
		}

		private void OnSubmit()
		{
			if (!AreInputsValid()) return;

			string error = viewModel.AddTicket(titleInput.Value, descriptionInput.Value, typeInput.Value);
			if(error != null)
			{
				submitButton.Error = error;
				Print();
				return;
			}

			closeFlag = true;
		}
	}
}
