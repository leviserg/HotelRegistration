using HotelRegistration.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private readonly Reservation _reservation;
        public string Room => _reservation.Room.ToString();
        public string VisitorName => _reservation.VisitorName;
        public string StartDate => _reservation.StartDate.ToString("yyyy-MM-dd");
        public string EndDate => _reservation.EndDate.ToString("yyyy-MM-dd");
        public int DaysReserved => _reservation.DaysReserved;

        public ReservationViewModel(Reservation reservation)
        {
             _reservation = reservation;
        }
    }
}
