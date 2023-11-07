using System;

namespace JiraClone.utils.consoleViewParts.options
{
    public interface IOption
    {
        public bool Selected { get; set; }
        public void UseKey(char c);
    }
}
