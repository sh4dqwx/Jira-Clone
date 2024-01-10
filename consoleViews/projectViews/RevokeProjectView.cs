using JiraClone.utils;
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
    public class RevokeProjectView: ConsoleView
    { 
        private ProjectsViewModel viewModel;

        private VerticalMenu shareProjectForm;
        private HorizontalMenu actionMenu;
        private Input projectNameInput, userLoginInput;
        private Button submitButton;
        private bool closeFlag = false;

        protected override void ResetView()
        {
            Clear();

            base.ResetView();
        }

        public RevokeProjectView(ProjectsViewModel viewModel)
        {
            this.viewModel = viewModel;

            shareProjectForm = new VerticalMenu("ODBIERANIE DOSTĘPU DO PROJEKTU", 4);
            actionMenu = new HorizontalMenu(2);

            projectNameInput = new Input("Nazwa projektu", validationRule: new RequiredRule());
            userLoginInput = new Input("Login użytkownika", validationRule: new RequiredRule());
            submitButton = new Button("Zatwierdź", OnSubmit);

            shareProjectForm.Add(projectNameInput);
            shareProjectForm.Add(userLoginInput);

            actionMenu.Add(submitButton);
            actionMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

            Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(shareProjectForm);
            Add(actionMenu);
        }

        private void EventHandler(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(e.PropertyName);
        }

        public Func<object>? Start()
        {
            ResetView();
            Print();

            while (true)
            {
                if (!Console.KeyAvailable)
                    continue;

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.I && keyInfo.Modifiers == ConsoleModifiers.Control)
                {
                    InterfaceController.CreateController().ChangeInterface();
                    EndLoop();
                    return null;
                }

                UseKey(keyInfo);

                if (closeFlag)
                {
                    closeFlag = false;
                    ResetView();
                    return null;
                }
                if (nextView != null)
                {
                    Func<object> funcToSend = nextView;
                    nextView = null;
                    return funcToSend;
                }
            }
        }

        private bool AreInputsValid()
        {
            bool areValid = true;
            if (!projectNameInput.Validate()) areValid = false;
            if (!userLoginInput.Validate()) areValid = false;
            return areValid;
        }

        private void OnSubmit()
        {
            if (!AreInputsValid()) return;

            string? error = viewModel.RevokeProject(projectNameInput.Value, userLoginInput.Value);
            if (error != null)
            {
                submitButton.Error = error;
                Print();
            }
            else closeFlag = true;
        }
    }
}
