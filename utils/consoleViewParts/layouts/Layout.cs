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

        public virtual void UnselectSelected()
        {
            if (selectedChild == -1)
                return;

            if (children[selectedChild] is Layout)
                ((Layout)children[selectedChild]).UnselectSelected();
            else if (children[selectedChild] is Option)
                ((Option)children[selectedChild]).Selected = false;

            selectedChild = -1;
        }

        public virtual bool NavigateTop()
        {
            selectedChild = 0;


            foreach (Printable child in children)
            {
                bool result = true;
                if (child is Layout)
                    result = ((Layout)child).NavigateTop();
                else if (child is Option)
                    ((Option)child).Selected = true;
                else
                    result = false;

                if (result)
                    break;

                selectedChild++;
            }

            if (selectedChild == children.Count)
            {
                selectedChild = -1;
                return false;
            }
            else
                return true;


        }

        public virtual bool NavigateUp()
        {
            if (selectedChild <= 0)
                return false;

            for (int i = selectedChild; i >= 0; selectedChild = --i)
            {
                Printable child = children[i];
                bool result = true;
                if (child is Layout)
                    result = ((Layout)child).NavigateUp();
                else if (child is Option)
                    ((Option)child).Selected = true;
                else
                    result = false;

                if (result)
                    break;

                if (i == selectedChild)
                    ((Layout)child).UnselectSelected();
            }

            if (selectedChild == -1)
                return false;
            else
                return true;
        }

        public virtual bool NavigateDown()
        {
            if (selectedChild == -1 || selectedChild == children.Count - 1)
                return false;

            UnselectSelected();

            for (int i = selectedChild; i < children.Count; selectedChild = ++i)
            {
                Printable child = children[i];
                bool result = true;
                if (child is Layout)
                    result = ((Layout)child).NavigateDown();
                else if (child is Option)
                    ((Option)child).Selected = true;
                else
                    result = false;

                if (result)
                    break;
            }

            if (selectedChild == -1)
                return false;
            else
                return true;
        }

        public void UseKey(char c)
        {
            if (selectedChild < 0)
                return;

            if (children[selectedChild] is Layout)
                ((Layout)children[selectedChild])?.UseKey(c);
            else if (children[selectedChild] is Option)
                ((Option)children[selectedChild])?.UseKey(c);
        }
    }
}
