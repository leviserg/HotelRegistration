using HotelRegistration.Commands;
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

        public ICommand? NavigateToMakeReservationPage { get; }

        public ReservationListViewModel()
        {
            _reservations = new ObservableCollection<ReservationViewModel>();
            NavigateToMakeReservationPage = new NavigateCommand();
        }
    }
}
