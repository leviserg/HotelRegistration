using HotelRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.Services.ReservationCreators
{
    public interface IReservationCreator
    {
        Task<int> CreateReservationAsync(Reservation reservation);
    }
}
