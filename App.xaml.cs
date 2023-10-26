using JiraClone.db;
using JiraClone.utils;
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
					options.UseSqlite();
				});
			});
			return builder.Build();
		}

        public App() {
            AppHost = BuildAppHost();

            MainWindow window = new();
            window.Show();
            InterfaceController.CreateController(window);
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
