using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db.dbmodels
{
    public record class AccountProject
	{
		public int ProjectId { get; set; }
		public int AccountId { get; set; }
		public bool IsOwner { get; set; }
	}
}
