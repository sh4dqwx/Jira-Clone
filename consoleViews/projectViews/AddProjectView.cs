using JiraClone.utils.consoleViewParts;
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

namespace JiraClone.views.ProjectViews
{
    public class AddProjectView : ConsoleView
    {
        private ProjectsViewModel viewModel;

        private VerticalMenu addProjectForm;
        private HorizontalMenu actionMenu;
        private Input nameInput;
        private Button submitButton;
        private bool closeFlag = false;

        protected override void ResetView()
        {
            Clear();
            base.ResetView();
        }

        public AddProjectView(ProjectsViewModel viewModel)
        {
            this.viewModel = viewModel;

            addProjectForm = new VerticalMenu("TWORZENIE PROJEKTU", 2);
            actionMenu = new HorizontalMenu(2);

            nameInput = new Input("Nazwa", validationRule: new RequiredRule());
            submitButton = new Button("Zatwierdź", OnSubmit);

            addProjectForm.Add(nameInput);

            actionMenu.Add(submitButton);
            actionMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

            Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(addProjectForm);
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

            viewModel.CreateProject(nameInput.Value);

            closeFlag = true;
        }
    }
}
