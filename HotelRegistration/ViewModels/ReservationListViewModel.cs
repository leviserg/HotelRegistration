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
        private readonly Hotel _hotel;
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public ICommand? NavigateToMakeReservationPage { get; }

        public ReservationListViewModel(Hotel hotel, ViewModelNavigationService navigationService)
        {
            _hotel = hotel;
            _reservations = new ObservableCollection<ReservationViewModel>();

            NavigateToMakeReservationPage = new NavigateCommand(navigationService);

            UpdateReservations();
        }

        private void UpdateReservations()
        {
            _reservations.Clear();
            var currentReservations = _hotel.GetReservations("Room", true);
            foreach (var reservation in currentReservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }

        }
    }
}
