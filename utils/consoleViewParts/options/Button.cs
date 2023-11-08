using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.consoleViewParts.options
{
	public class Button : Option
	{
		public Button(int width, string name, Action callback) : base(width, name, callback)
		{
		}
	}
}
