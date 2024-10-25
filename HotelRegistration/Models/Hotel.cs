using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.Models
{
    public class Hotel
    {
        private readonly ReservationBook _reservationBook;
        private const string defaultSortKey = nameof(Room);
        private const bool defaultDescSortOrder = false;

        public string Name { get; }

        public Hotel(string name)
        {
            _reservationBook = new ReservationBook();
            Name = name;
        }

        /// <summary>
        /// Get the reservations for a user.
        /// </summary>
        /// <param name="sortKey">SortKey : Room, StartDate, EndDate, VisitorName, DaysReserved</param>
        /// <param name="sortDesc">SortOrder Asc By Default</param>
        /// <returns>Reservations sorted</returns>
        public IEnumerable<Reservation> GetReservations(string sortKey = defaultSortKey, bool sortDesc = defaultDescSortOrder)
        {
            return _reservationBook.GetReservations(sortKey, sortDesc);
        }
        /// <summary>
        /// Get the reservations for a user.
        /// </summary>
        /// <param name="visitor">The incoming visitor.</param>
        /// <returns>The reservations for a user.</returns>
        public IEnumerable<Reservation> GetReservationsByVisitor(Visitor visitor)
        {
            return _reservationBook.GetReservationsByVisitor(visitor);
        }
        /// <summary>
        /// Get the reservations for a specific room.
        /// </summary>
        /// <param name="room">Room selected.</param>
        /// <returns>The reservations for a specific room.</returns>
        public IEnumerable<Reservation> GetReservationsByRoom(Room room)
        {
            return _reservationBook.GetReservationsByRoom(room);
        }
        /// <summary>
        /// Make a reservation.
        /// </summary>
        /// <param name="reservation">Incoming reservation.</param>
        /// <exception cref="ReservationConflictException"
        public void MakeReservation(Reservation reservation) { 
            _reservationBook.AddReservation(reservation);
        }
    }
}
