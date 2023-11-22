﻿using JiraClone.db;
using JiraClone.db.repositories;
using JiraClone.models;
using JiraClone.utils;
using JiraClone.viewmodels;
using JiraClone.views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace JiraClone
{
    public partial class App : Application
    {
        private static IHost? AppHost { get; set; }

        private IHost BuildAppHost()
        {
			var builder = Host.CreateDefaultBuilder();
			builder.ConfigureServices((context, services) =>
			{
				services.AddDbContext<SqliteDbContext>(options =>
				{
					//dodać connection string
					options.UseSqlite($"Data Source={AppDomain.CurrentDomain.BaseDirectory}/sqlite.db");
				});

                //States
                services.AddSingleton<ApplicationState>();

                //Views
                services.AddSingleton<WelcomeView>();
                services.AddSingleton<LoginView>();
                services.AddSingleton<RegisterView>();
                //ViewModels
                services.AddSingleton<LoginViewModel>();
                services.AddSingleton<RegisterViewModel>();
                services.AddSingleton<ProjectsViewModel>();
                //Repositories
                services.AddSingleton<IAccountRepository, AccountRepository>();
                services.AddSingleton<IProjectRepository, ProjectRepository>();
                services.AddSingleton<ITicketRepository, TicketRepository>();
                services.AddSingleton<IStatusRepository, StatusRepository>();
                services.AddSingleton<ICommentRepository, CommentRepository>();

				using var serviceProvider = services.BuildServiceProvider();
				var dbContext = serviceProvider.GetRequiredService<SqliteDbContext>();
				dbContext.Database.Migrate();
			});
            builder.UseDefaultServiceProvider(options => options.ValidateScopes = false);
			return builder.Build();
		}

        public App() {
            AppHost = BuildAppHost();

            MainWindow window = new();
            InterfaceController.CreateController(window);

            WelcomeView console = AppHost.Services.GetRequiredService<WelcomeView>();
            console.Start();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}
