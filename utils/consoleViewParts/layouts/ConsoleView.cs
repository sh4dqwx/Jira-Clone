using JiraClone.utils.consoleViewParts.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace JiraClone.utils.consoleViewParts.layouts
{
    public class ConsoleView : Layout, IMenu
	{
		protected List<ISelectable> selectableChildren;
		protected readonly object consoleLock = new();
		private Thread loopThread;
		private Action? actionForLoop;

		protected virtual void ResetView()
		{
			lock (consoleLock)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.White;
				Console.CursorVisible = false;
			}

			SelectTop();
		}

		public ConsoleView(): base()
		{
			Height = Constants.WINDOW_HEIGHT;
			Width = Constants.WINDOW_WIDTH;
			selectableChildren = new();
			loopThread = new Thread(Loop);
		}

		public override void Print()
		{
			lock (consoleLock)
			{
				int cursorLeft = Left;
				int cursorTop = Top;

				foreach (Printable child in children)
				{
					Console.SetCursorPosition(cursorLeft, cursorTop);
					child.Left = (Width - child.Width) / 2 + cursorLeft;
					child.Top = cursorTop;
					child.Print();

					cursorTop += child.Height + 1;
				}

				if (selectedChild != -1) ((Printable)selectableChildren[selectedChild]).Print();
			}
		}

		public override void Add(Printable child)
		{
			base.Add(child);
			if (child is IMenu) selectableChildren.Add((IMenu)child);
		}

		public override void Remove(Printable child)
		{
			base.Remove(child);
			if (child is IMenu) selectableChildren.Remove((IMenu)child);
		}

		public void UnselectSelected()
		{
			if (selectedChild == -1)
				return;

			if (selectableChildren[selectedChild] is Option)
			{
				lock (consoleLock)
				{
					selectableChildren[selectedChild].Selected = false;
					((Printable)selectableChildren[selectedChild]).Print();
				}
			}
			else ((IMenu)selectableChildren[selectedChild]).UnselectSelected();

			selectableChildren[selectedChild].Selected = false;
		}

		public bool SelectTop()
		{
			if (selectableChildren.Count == 0)
				return false;

			UnselectSelected();

			selectedChild = 0;

			for (int i = 0; i < selectableChildren.Count; i++, selectedChild = i)
            {
                IMenu child = (IMenu)selectableChildren[i];
                if (child is Option)
                {
					lock (consoleLock)
					{
						child.Selected = true;
						((Printable)child).Print();
					}
                    return true;
                }

                if (child.SelectTop())
                    return true;
            }

			return false;
        }

		public bool SelectBottom()
		{
			if (selectableChildren.Count == 0) 
				return false;

			UnselectSelected();

			selectedChild = selectableChildren.Count - 1;

            for (int i = selectableChildren.Count - 1; i >= 0; i--, selectedChild = i)
            {
				IMenu child = (IMenu)selectableChildren[i];
                if (child is Option)
                {
					lock (consoleLock)
					{
						child.Selected = true;
						((Printable)child).Print();
					}
                    return true;
                }

                if (child.SelectBottom())
                    return true;
            }

			return false;
        }

		public bool SelectPrevious()
		{
			if (selectedChild == -1) return false;

			for (int i = selectedChild - 1; i >= 0; i++)
			{
				if (selectableChildren[i].CanSelect())
				{
					UnselectSelected();
					selectedChild = i;
					if (selectableChildren[selectedChild] is Option)
					{
						lock (consoleLock)
						{
							selectableChildren[selectedChild].Selected = true;
							((Printable)selectableChildren[selectedChild]).Print();
						}
						return true;
					}
					bool result = ((IMenu)selectableChildren[selectedChild]).SelectBottom();
					if (result)
						lock (consoleLock) ((Printable)selectableChildren[selectedChild]).Print();
					return result;
				}
			}
			return false;
		}

		public bool SelectNext()
		{
			if (selectedChild == -1) return false;

			for (int i = selectedChild + 1; i < selectableChildren.Count; i++)
			{
				if (selectableChildren[i].CanSelect())
				{
					UnselectSelected();
					selectedChild = i;
					if (selectableChildren[selectedChild] is Option)
					{
						lock(consoleLock)
						{
							selectableChildren[selectedChild].Selected = true;
							((Printable)selectableChildren[selectedChild]).Print();
						}
						return true;
					}
					bool result = ((IMenu)selectableChildren[selectedChild]).SelectTop();
					if (result)
						lock (consoleLock) ((Printable)selectableChildren[selectedChild]).Print();
					return result;
				}
			}
			return false;
		}

		public bool UseKey(ConsoleKeyInfo c)
		{
			if (selectedChild < 0) return false;

			switch (c.Key)
			{
				case ConsoleKey.UpArrow:
					if (UseKeyOnChild(c, selectableChildren[selectedChild])) return true;
					return SelectPrevious();

				case ConsoleKey.DownArrow:
				case ConsoleKey.Tab:
					if (UseKeyOnChild(c, selectableChildren[selectedChild])) return true;
					return SelectNext();

                default:
					return UseKeyOnChild(c, selectableChildren[selectedChild]);
			}
		}

		private bool UseKeyOnChild(ConsoleKeyInfo consoleKey, ISelectable child)
		{
			if(consoleKey.Key == ConsoleKey.Enter)
				return child.UseKey(consoleKey);

			bool returnValue;
			lock (consoleLock) returnValue = child.UseKey(consoleKey);
			return returnValue;
		}

		public bool CanSelect()
		{
			foreach (ISelectable child in selectableChildren)
			{
				if (child.CanSelect()) return true;
			}
			return false;
		}

        public override void Clear()
        {
            foreach (ISelectable child in selectableChildren)
			{
				child.Clear();
			}
        }

        public void StartNewConsoleView(Action action)
		{
			Action? actionForLoop = this.actionForLoop;
			if (loopThread.IsAlive)
				EndLoop();

            action();

            ResetView();
            Print();

            if (actionForLoop != null && !loopThread.IsAlive)
			{
				StartLoop(actionForLoop);
			}
		}

		public void StartLoop(Action action)
		{
			if (actionForLoop != null || loopThread.IsAlive)
				throw new Exception("Loop already started");

			actionForLoop = action;
			if(loopThread.ThreadState == ThreadState.Unstarted)
				loopThread.Start();
			else
			{
                loopThread = new Thread(Loop);
				loopThread.Start();
            }
		}

		public void EndLoop()
		{
			if (actionForLoop == null && !loopThread.IsAlive)
				return;

			lock (consoleLock) actionForLoop = null;
		}

		private void Loop()
		{
			while (actionForLoop != null)
			{
				Thread.Sleep(500);
				lock(consoleLock) actionForLoop?.Invoke();
			}
		} 

		public bool Selected { get; set; }
	}
}
