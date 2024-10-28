using HotelRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.Exceptions
{
    public class ReservationConflictException : Exception
    {

        public Reservation BookedReservation { get; }
        public Reservation IncomingReservation { get; }

        public ReservationConflictException(Reservation incomingReservation)
        {
            IncomingReservation = incomingReservation;
        }
        public ReservationConflictException(Reservation bookedReservation, Reservation incomingReservation)
        {
            BookedReservation = bookedReservation;
            IncomingReservation = incomingReservation;
        }

        public ReservationConflictException(string? message, Reservation bookedReservation, Reservation incomingReservation) : base(message)
        {
            BookedReservation = bookedReservation;
            IncomingReservation = incomingReservation;
        }

        public ReservationConflictException(string? message, Exception? innerException, Reservation bookedReservation, Reservation incomingReservation) : base(message, innerException)
        {
            BookedReservation = bookedReservation;
            IncomingReservation = incomingReservation;
        }

    }
}
