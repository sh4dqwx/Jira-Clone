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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JiraClone.views.ProjectViews
{
    public class RemoveProjectView : ConsoleView
    {
        private ProjectsViewModel viewModel;

        private VerticalMenu removeProjectForm;
        private HorizontalMenu actionMenu;
        private Input nameInput;
        private Button submitButton;
        private bool closeFlag = false;

        protected override void ResetView()
        {
            Clear();

            base.ResetView();
        }

        public RemoveProjectView(ProjectsViewModel viewModel)
        {
            this.viewModel = viewModel;

            removeProjectForm = new VerticalMenu("USUWANIE PROJEKTU", 3);
            actionMenu = new HorizontalMenu(2);

            nameInput = new Input("Nazwa", validationRule: new RequiredRule());
            submitButton = new Button("Zatwierdź", OnSubmit);

            removeProjectForm.Add(nameInput);

            actionMenu.Add(submitButton);
            actionMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

            Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(removeProjectForm);
            Add(actionMenu);
        }

        private void EventHandler(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(e.PropertyName);
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
            if (!nameInput.Validate()) areValid = false;
            return areValid;
        }

        private void OnSubmit()
        {
            if (!AreInputsValid()) return;

            string? error = viewModel.RemoveProject(nameInput.Value);

            if (error != null)
            {
                submitButton.Error = error;
                Print();
            }
            else closeFlag = true;
        }
    }
}
