using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.dbmodels
{
	public class AccountProject
	{
		public int IdProject { get; set; }
		public int IdAccount { get; set; }
		public bool IsOwner { get; set; }
	}
}
