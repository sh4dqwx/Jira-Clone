﻿using JiraClone.utils.consoleViewParts;
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

namespace JiraClone.views
{
    public class AddProjectView: ConsoleView
    {
        private ProjectsViewModel viewModel;

        private VerticalMenu menu;
        private Input nameInput;
        private Button submitButton;
        private bool closeFlag = false;

        private void ResetView()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;

            nameInput.Clear();

            Print();
            SelectTop();
        }

        public AddProjectView(ProjectsViewModel viewModel)
        {
            this.viewModel = viewModel;
            
            menu = new VerticalMenu(3);

            nameInput = new Input("Nazwa", validationRule: new RequiredRule());
            submitButton = new Button("Zatwierdź", OnSubmit);

            menu.Add(nameInput);
            menu.Add(submitButton);
            menu.Add(new Button("Powrót", () => { closeFlag = true; }));

            Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
            Add(new Text("TWORZENIE PROJEKTU"));
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
