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

        public Hotel(string name, ReservationBook reservationBook)
        {
            _reservationBook = reservationBook;
            Name = name;
        }

        /// <summary>
        /// Get the reservations for a user.
        /// </summary>
        /// <param name="sortKey">SortKey : Room, StartDate, EndDate, VisitorName, DaysReserved</param>
        /// <param name="sortDesc">SortOrder Asc By Default</param>
        /// <returns>Reservations sorted</returns>
        public async Task<IEnumerable<Reservation>> GetReservations(string sortKey = defaultSortKey, bool sortDesc = defaultDescSortOrder)
        {
            return await _reservationBook.GetReservations(sortKey, sortDesc);
        }
        /// <summary>
        /// Get the reservations for a user.
        /// </summary>
        /// <param name="visitor">The incoming visitor.</param>
        /// <returns>The reservations for a user.</returns>
        public async Task<IEnumerable<Reservation>> GetReservationsByVisitor(Visitor visitor)
        {
            return await _reservationBook.GetReservationsByVisitor(visitor);
        }
        /// <summary>
        /// Get the reservations for a specific room.
        /// </summary>
        /// <param name="room">Room selected.</param>
        /// <returns>The reservations for a specific room.</returns>
        public async Task<IEnumerable<Reservation>> GetReservationsByRoom(Room room)
        {
            return await _reservationBook.GetReservationsByRoom(room);
        }
        /// <summary>
        /// Make a reservation.
        /// </summary>
        /// <param name="reservation">Incoming reservation.</param>
        /// <exception cref="ReservationConflictException"
        public async Task<int> MakeReservation(Reservation reservation) { 
            return await _reservationBook.AddReservation(reservation);
        }
    }
}
