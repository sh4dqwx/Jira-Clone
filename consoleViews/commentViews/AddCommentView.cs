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

namespace JiraClone.views.CommentViews
{
	public class AddCommentView : ConsoleView
	{
		private CommentsViewModel viewModel;

		private VerticalMenu addCommentForm;
		private HorizontalMenu actionMenu;
		private Input contentInput;
		private Button submitButton;
		private bool closeFlag = false;

		protected override void ResetView()
		{
			Clear();
			base.ResetView();
		}

		public AddCommentView(CommentsViewModel viewModel)
		{
			this.viewModel = viewModel;

			addCommentForm = new VerticalMenu("DODAWANIE KOMENTARZA", 2);
			actionMenu = new HorizontalMenu(2);

			contentInput = new Input("Treść", validationRule: new RequiredRule());
			submitButton = new Button("Zatwierdź", OnSubmit);

			addCommentForm.Add(contentInput);

			actionMenu.Add(submitButton);
			actionMenu.Add(new Button("Powrót", () => { closeFlag = true; }));

			Add(new Text("Nacisnij CTRL+I aby zmienic interfejs"));
			Add(addCommentForm);
			Add(actionMenu);
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
			if (!contentInput.Validate()) areValid = false;
			return areValid;
		}

		private void OnSubmit()
		{
			if (!AreInputsValid()) return;

			viewModel.AddComment(contentInput.Value);

			closeFlag = true;
		}
	}
}
