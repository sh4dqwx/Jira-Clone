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
		public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options) { }

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
				.HasKey(a => a.Id);
			modelBuilder.Entity<Account>()
				.Property(a => a.Login)
				.IsRequired();
			modelBuilder.Entity<Account>()
				.Property(a => a.Password)
				.IsRequired();
			modelBuilder.Entity<Account>()
				.Property(a => a.Email)
				.IsRequired();
			modelBuilder.Entity<Account>()
				.Property(a => a.Name)
				.IsRequired();
			modelBuilder.Entity<Account>()
				.Property(a => a.Surname)
				.IsRequired();
			modelBuilder.Entity<Account>()
				.Property(a => a.CreationTimestamp)
				.IsRequired();

			modelBuilder.Entity<Project>()
				.HasKey(p => p.Id);
			modelBuilder.Entity<Project>()
				.Property(p => p.Name)
				.IsRequired();
			modelBuilder.Entity<Project>()
				.Property(p => p.CreationTimestamp)
				.IsRequired();

			modelBuilder.Entity<Account>()
				.HasMany(a => a.Projects)
				.WithMany(p => p.Accounts)
				.UsingEntity<AccountProject>();

			modelBuilder.Entity<Ticket>()
				.HasKey(t => t.Id);
			modelBuilder.Entity<Ticket>()
				.Property(t => t.Title)
				.IsRequired();
			modelBuilder.Entity<Ticket>()
				.Property(t => t.Description)
				.IsRequired(false);
			modelBuilder.Entity<Ticket>()
				.Property(t => t.Type)
				.IsRequired();
			modelBuilder.Entity<Ticket>()
				.Property(t => t.Code)
				.IsRequired();
			modelBuilder.Entity<Ticket>()
				.HasOne(t => t.Project)
				.WithMany(p => p.Tickets)
				.HasForeignKey(t => t.ProjectId)
				.IsRequired();
			modelBuilder.Entity<Ticket>()
				.HasOne(t => t.Reporter)
				.WithMany(a => a.ReporterTickets)
				.HasForeignKey(t => t.ReporterId)
				.IsRequired();
			modelBuilder.Entity<Ticket>()
				.HasOne(t => t.Asignee)
				.WithMany(a => a.AsigneeTickets)
				.HasForeignKey(t => t.AsigneeId)
				.IsRequired(false);
			modelBuilder.Entity<Ticket>()
				.HasOne(t => t.Status)
				.WithMany(s => s.Tickets)
				.HasForeignKey(t => t.StatusId)
				.IsRequired();

			modelBuilder.Entity<Status>()
				.HasKey(s => s.Id);
			modelBuilder.Entity<Status>()
				.Property(s => s.Name)
				.IsRequired();
			modelBuilder.Entity<Status>()
				.Property(s => s.Order)
				.IsRequired();
			modelBuilder.Entity<Status>()
				.HasOne(s => s.Project)
				.WithMany(p => p.Statuses)
				.HasForeignKey(s => s.ProjectId)
				.IsRequired();

			modelBuilder.Entity<Comment>()
				.HasKey(c => c.Id);
			modelBuilder.Entity<Comment>()
				.Property(c => c.Content)
				.IsRequired();
			modelBuilder.Entity<Comment>()
				.HasOne(c => c.Ticket)
				.WithMany(t => t.Comments)
				.HasForeignKey(c => c.TicketId)
				.IsRequired();
			modelBuilder.Entity<Comment>()
				.HasOne(c => c.Account)
				.WithMany(a => a.Comments)
				.HasForeignKey(c => c.AccountId)
				.IsRequired();

			CreateInitialData(modelBuilder);
		}

		private void CreateInitialData(ModelBuilder modelBuilder)
		{
			Account adminAccount = new Account
			{
				Id = 1,
				Login = "admin",
				Password = "admin",
				Email = "admin@test.com",
				Name = "Jan",
				Surname = "Kowalski",
				CreationTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
			};

			modelBuilder.Entity<Account>()
				.HasData(adminAccount);
		}
	}
}
