using JiraClone.db.dbmodels;
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

		public DbSet<Account> Accounts { get; set; }
		public DbSet<AccountProject> AccountProjects { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Ticket> Tickets { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<Comment> Comments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Account>()
				.ToTable("Account");
			modelBuilder.Entity<Account>()
				.HasKey(a => a.Id);
			modelBuilder.Entity<Account>()
				.Property(a => a.Id)
				.ValueGeneratedOnAdd();

			modelBuilder.Entity<Project>()
				.ToTable("Project");
			modelBuilder.Entity<Project>()
				.HasKey(p => p.Id);
			modelBuilder.Entity<Project>()
				.Property(p => p.Id)
				.ValueGeneratedOnAdd();

			modelBuilder.Entity<AccountProject>()
				.ToTable("AccountProject");
			modelBuilder.Entity<AccountProject>()
				.HasKey(ap => new { ap.IdAccount, ap.IdProject });
			modelBuilder.Entity<AccountProject>()
				.HasOne(ap => ap.Account)
				.WithMany(a => a.AccountProjects)
				.HasForeignKey(ap => ap.IdAccount);
			modelBuilder.Entity<AccountProject>()
				.HasOne(ap => ap.Project)
				.WithMany(p => p.AccountProjects)
				.HasForeignKey(ap => ap.Project);

			modelBuilder.Entity<Ticket>()
				.ToTable("Ticket");
			modelBuilder.Entity<Ticket>()
				.HasKey(t => t.Id);
			modelBuilder.Entity<Ticket>()
				.Property(t => t.Id)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<Ticket>()
				.HasOne(t => t.Project)
				.WithMany(p => p.Tickets)
				.HasForeignKey(t => t.IdProject);
			modelBuilder.Entity<Ticket>()
				.HasOne(t => t.Reporter)
				.WithMany(a => a.Tickets)
				.HasForeignKey(t => t.IdReporter);
			modelBuilder.Entity<Ticket>()
				.HasOne(t => t.Asignee)
				.WithMany(a => a.Tickets)
				.HasForeignKey(t => t.IdAsignee);
			modelBuilder.Entity<Ticket>()
				.HasOne(t => t.Status)
				.WithMany(s => s.Tickets)
				.HasForeignKey(t => t.IdStatus);

			modelBuilder.Entity<Status>()
				.ToTable("Status");
			modelBuilder.Entity<Status>()
				.HasKey(s => s.Id);
			modelBuilder.Entity<Status>()
				.Property(s => s.Id)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<Status>()
				.HasOne(s => s.Project)
				.WithMany(p => p.Statuses)
				.HasForeignKey(s => s.IdProject);

			modelBuilder.Entity<Comment>()
				.ToTable("Comment");
			modelBuilder.Entity<Comment>()
				.HasKey(c => c.Id);
			modelBuilder.Entity<Comment>()
				.Property(c => c.Id)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<Comment>()
				.HasOne(c => c.Ticket)
				.WithMany(t => t.Comments)
				.HasForeignKey(c => c.IdTicket);
			modelBuilder.Entity<Comment>()
				.HasOne(c => c.Account)
				.WithMany(a => a.Comments)
				.HasForeignKey(c => c.IdAccount);
		}
	}
}
