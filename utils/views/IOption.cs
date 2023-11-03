using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.views
{
	public interface IOption
	{
		public void Select();
		public void Unselect();
	}
}
