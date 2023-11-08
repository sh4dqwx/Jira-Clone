using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.layouts
{
    public abstract class CompoundPrintable : Printable
    {
        protected List<Printable> children;

        protected CompoundPrintable(int height, int width) : base(height, width)
        {
            children = new List<Printable>();
        }

        public virtual void Add(Printable child) { children.Add(child); }

        public virtual void Remove(Printable child) { children.Remove(child); }
    }
}
