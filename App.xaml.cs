﻿using JiraClone.db;
using JiraClone.db.repositories;
using JiraClone.utils;
using JiraClone.views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
					options.UseSqlite("Data Source=sqlite.db");
				});
                services.AddSingleton<IAccountRepository, AccountRepository>();
			});
			return builder.Build();
		}

        public App() {
            AppHost = BuildAppHost();

            MainWindow window = new();
            InterfaceController.CreateController(window);

            WelcomeView console = new();
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
