using HotelRegistration.Models;
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

        public ICommand NavigateToMakeReservationPage { get; }

        public ReservationListViewModel()
        {
            _reservations = new ObservableCollection<ReservationViewModel>();

            _reservations.Add(new ReservationViewModel(new Reservation(new Room(1, 1), "John Doe", DateTime.Now, DateTime.Now.AddDays(2))));
            _reservations.Add(new ReservationViewModel(new Reservation(new Room(1, 2), "Jane Smith", DateTime.Now, DateTime.Now.AddDays(3))));
            _reservations.Add(new ReservationViewModel(new Reservation(new Room(1, 3), "Nick Smart", DateTime.Now, DateTime.Now.AddDays(4))));
            _reservations.Add(new ReservationViewModel(new Reservation(new Room(1, 4), "Jack Fun", DateTime.Now, DateTime.Now.AddDays(5))));
        }
    }
}
