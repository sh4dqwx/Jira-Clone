using System;

namespace JiraClone.utils.consoleViewParts.options
{
    public interface ITextOption : IOption
    {
        public string Value { get; }
    }
}
