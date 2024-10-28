using HotelRegistration.Commands;
using HotelRegistration.Models;
using HotelRegistration.Services;
using HotelRegistration.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelRegistration.ViewModels
{
    public class ReservationListViewModel : ViewModelBase
    {

        private readonly ObservableCollection<ReservationViewModel> _reservations;
        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public ICommand? NavigateToMakeReservationPage { get; }
        public ICommand? LoadCommand { get; }

        public ReservationListViewModel(Hotel hotel, ViewModelNavigationService navigationService)
        {
            _reservations = new ObservableCollection<ReservationViewModel>();
            LoadCommand = new LoadReservationsCommand(hotel, this);
            NavigateToMakeReservationPage = new NavigateCommand(navigationService);
        }

        public static ReservationListViewModel LoadViewModel(Hotel hotel, ViewModelNavigationService navigationService)
        {
            ReservationListViewModel viewModel = new ReservationListViewModel(hotel, navigationService);
            viewModel.LoadCommand.Execute(null);
            return viewModel;
        }


        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();

            foreach (var reservation in reservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }

        }

    }
}
