using HotelRegistration.DbContexts;
using HotelRegistration.Exceptions;
using HotelRegistration.Models;
using HotelRegistration.Services;
using HotelRegistration.Services.ReservationCreators;
using HotelRegistration.Services.ReservationProviders;
using HotelRegistration.Services.Validators;
using HotelRegistration.Stores;
using HotelRegistration.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;

namespace HotelRegistration
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Hotel _hotel;
        private readonly NavigationStore _navigationStore;
        private const string DB_CONNECTION_STRING = "Server=XXXXXXXXX;Database=HotelReservation;Uid=XXXXXXXX;Pwd=XXXXXXXXX;TrustServerCertificate=XXXXXXXXX;";
        private ReservationDbContextFactory _dbContextFactory => new ReservationDbContextFactory(DB_CONNECTION_STRING);

        
        public App()
        {
            IReservationCreator reservationCreator = new ReservationCreator(_dbContextFactory);
            IReservationProvider reservationProvider = new ReservationProvider(_dbContextFactory);
            IConflictValidator conflictValidator = new ConflictValidator(_dbContextFactory);

            ReservationBook reservationBook = new ReservationBook(reservationProvider, reservationCreator, conflictValidator);

            _hotel = new Hotel("Mongo Suites", reservationBook);
            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {

            using(ReservationDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!dbContext.HasTable(nameof(dbContext.Reservations)))
                {
                    dbContext.Database.Migrate();
                }
            }

            _navigationStore.CurrentViewModel = NavigateToReservationViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();
            base.OnStartup(e);
        }

        private MakeReservationViewModel NavigateToMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotel, new ViewModelNavigationService(_navigationStore, NavigateToReservationViewModel));
        }

        private ReservationListViewModel NavigateToReservationViewModel()
        {
            return ReservationListViewModel.LoadViewModel(_hotel, new ViewModelNavigationService(_navigationStore, NavigateToMakeReservationViewModel));
        }

    }

}
