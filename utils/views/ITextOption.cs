using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils.views
{
	public interface ITextOption : IOption
	{
		public string Value { get; }
	}
}
