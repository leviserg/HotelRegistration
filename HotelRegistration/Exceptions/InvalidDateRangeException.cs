using HotelRegistration.Models;

namespace HotelRegistration.Exceptions
{
    public class InvalidDateRangeException : Exception
    {
        public Reservation Reservation { get; }

        public InvalidDateRangeException(Reservation reservation)
        {
            Reservation = reservation;
        }
    }
}
