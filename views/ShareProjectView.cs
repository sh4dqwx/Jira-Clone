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

namespace JiraClone.views
{
    public class ShareProjectView: ConsoleView
    {
        private ProjectsViewModel viewModel;

        private VerticalMenu menu;
        private Input projectNameInput, userLoginInput;
        private Button submitButton;
        private bool closeFlag = false;

        private void ResetView()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;

            projectNameInput.Clear();
            userLoginInput.Clear();

            Print();
            SelectTop();
        }

        public ShareProjectView(ProjectsViewModel viewModel)
        {
            this.viewModel = viewModel;

            menu = new VerticalMenu(4);

            projectNameInput = new Input("Nazwa projektu", validationRule: new RequiredRule());
            userLoginInput = new Input("Login użytkownika", validationRule: new RequiredRule());
            submitButton = new Button("Zatwierdź", OnSubmit);

            menu.Add(projectNameInput);
            menu.Add(userLoginInput);
            menu.Add(submitButton);
            menu.Add(new Button("Powrót", () => { closeFlag = true; }));

            Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(new Text("UDOSTĘPNIANIE PROJEKTU"));
            Add(menu);
        }

        private void EventHandler(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(e.PropertyName);
        }

        public void Start()
        {
            ResetView();

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
            if (!projectNameInput.Validate()) areValid = false;
            if (!userLoginInput.Validate()) areValid = false;
            return areValid;
        }

        private void OnSubmit()
        {
            if (!AreInputsValid()) return;

            string? error = viewModel.ShareProject(projectNameInput.Value, userLoginInput.Value);
            if (error != null)
            {
                submitButton.Error = error;
                submitButton.Print();
            }
            else closeFlag = true;
        }
    }
}
