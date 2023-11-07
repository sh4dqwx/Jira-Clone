using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts
{
    public abstract class Printable : IPrintable
    {
        protected int left;
        protected int top;
        protected int width;

        protected Printable(int width)
        {
            this.width = width;
        }

        public int Width { get => width; }

        public virtual void Print(int left, int top)
        { 
            this.left = left;
            this.top = top;
        }
    
        public void Print() { Print(left, top); }
    }
}
