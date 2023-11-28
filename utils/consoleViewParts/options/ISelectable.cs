using System;

namespace JiraClone.utils.consoleViewParts.options
{
    public interface ISelectable
    {
        public bool Selected { get; set; }
        public bool CanSelect();
        public void Clear();
        public bool UseKey(ConsoleKeyInfo c);
    }
}
