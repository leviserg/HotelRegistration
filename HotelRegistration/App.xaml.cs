using HotelRegistration.Exceptions;
using HotelRegistration.Models;
using HotelRegistration.Services;
using HotelRegistration.Stores;
using HotelRegistration.ViewModels;
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
        public App()
        {
            _hotel = new Hotel("Mongo Suites");
            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {

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
            return new ReservationListViewModel(_hotel, new ViewModelNavigationService(_navigationStore, NavigateToMakeReservationViewModel));
        }
    }

}
