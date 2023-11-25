using JiraClone.utils.consoleViewParts.options;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.layouts
{
    public interface IMenu : ISelectable
    {
        public void UnselectSelected();
        public bool SelectTop();
        public bool SelectBottom();
        public bool SelectNext();
        public bool SelectPrevious();
    }
}
