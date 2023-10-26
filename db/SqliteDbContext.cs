using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.db
{
	public class SqliteDbContext : DbContext
	{
		public SqliteDbContext(DbContextOptions<SqliteDbContext> options)
			: base(options)
		{
			
		}
	}
}
