using JiraClone.utils.consoleViewParts.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.layouts
{
    public abstract class Layout : Printable
    {
        protected List<Printable> children;
        protected int selectedChild;

        protected Layout() : base()
        {
            children = new List<Printable>();
            selectedChild = -1;
        }

        public virtual void Add(Printable child)
        {
            children.Add(child);
        }

        public virtual void Remove(Printable child)
        {
            children.Remove(child);
        }

        public abstract void Clear();

        public virtual void ClearChildren()
        {
            children.Clear();
        }
    }
}
