using System;

namespace JiraClone.utils.consoleViewParts.options
{
    public interface ITextOption : ISelectable
    {
        public string Value { get; }
    }
}
