using HotelRegistration.DbContexts;
using HotelRegistration.Exceptions;
using HotelRegistration.Extensions;
using HotelRegistration.Models;
using HotelRegistration.Services;
using HotelRegistration.Services.ReservationCreators;
using HotelRegistration.Services.ReservationProviders;
using HotelRegistration.Services.Validators;
using HotelRegistration.Stores;
using HotelRegistration.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;

namespace HotelRegistration
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly IHost _host;
        private const string connectionStringKey = "ReservationsDbConnection";

        public App()
        {

            _host = Host.CreateDefaultBuilder()
                .AddViewModels()
                .ConfigureServices((hostContext, services) =>
                {

                    // string connectionString = hostContext.Configuration.GetConnectionString(connectionStringKey);
                    string connectionString = Environment.GetEnvironmentVariable(connectionStringKey);
                    string hotelName = hostContext.Configuration.GetValue<string>("HotelName");

                    services.AddSingleton<ReservationDbContextFactory>(new ReservationDbContextFactory(connectionString));
                    services.AddSingleton<IReservationProvider, ReservationProvider>();
                    services.AddSingleton<IReservationCreator, ReservationCreator>();
                    services.AddSingleton<IConflictValidator, ConflictValidator>();

                    services.AddTransient<ReservationBook>(); // one hotel can have multiple reservation books, so 

                    services.AddSingleton<Hotel>(
                        (s) => new Hotel(hotelName, s.GetRequiredService<ReservationBook>())
                    );

                    services.AddSingleton<ReservationCacheStore>();
                    services.AddSingleton<NavigationStore>();

                    services.AddSingleton<MainWindow>(
                        (s) => new MainWindow()
                        {
                            DataContext = s.GetRequiredService<MainViewModel>()
                        }
                    );

                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            _host.Start();

            NavigationStore navigationStore = _host.Services.GetRequiredService<NavigationStore>();

            /*
            using (ReservationDbContext dbContext = _host.Services.GetRequiredService<ReservationDbContextFactory>().CreateDbContext())
            {
                if (!dbContext.HasTable(nameof(dbContext.Reservations)))
                {
                    dbContext.Database.Migrate();
                }
            }
            */

            ViewModelNavigationService<ReservationListViewModel> navigationService = _host.Services.GetService<ViewModelNavigationService<ReservationListViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Title = _host.Services.GetRequiredService<Hotel>().Name;
            MainWindow.Show();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();
            base.OnExit(e); 
        }

    }
}
