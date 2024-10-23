using HotelRegistration.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.Models
{
    public class ReservationBook
    {
        private readonly List<Reservation> _reservations;

        public ReservationBook()
        {
            _reservations = new List<Reservation>();
        }

        public IEnumerable<Reservation> GetReservationsByVisitor(Visitor visitor) { 
            return _reservations.Where(r => r.VisitorName == visitor.Name);
        }

        public IEnumerable<Reservation> GetReservationsByRoom(Room room)
        {
            return _reservations.Where(r => r.Room.Equals(room));
        }

        public void AddReservation(Reservation reservation) {

            _reservations.ForEach(r =>
            {
                if (r.Conflicts(reservation)) {
                    throw new ReservationConflictException(r, reservation);
                }
            });

            _reservations.Add(reservation);
        }
    }
}
