using System;

namespace JiraClone.utils.consoleViewParts
{
	public interface IOption
	{
		public string Title { get; }
		public void Select();
		public void Unselect();
		public void UseKey(char c);
	}
}
