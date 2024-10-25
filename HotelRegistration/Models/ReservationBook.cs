using HotelRegistration.Exceptions;
using HotelRegistration.ViewModels;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.Models
{
    public class ReservationBook
    {
        private readonly List<Reservation> _reservations;
        private const string defaultSortKey = nameof(Room);

        public ReservationBook()
        {
            _reservations = new List<Reservation>();
        }

        public IEnumerable<Reservation> GetReservations(string sortKey = defaultSortKey, bool sortDesc = false)
        {
            
            PropertyInfo prop = typeof(Reservation).GetProperty(sortKey);
            if (prop == null)
                prop = typeof(Reservation).GetProperty(defaultSortKey);

            if (sortDesc) {
                return _reservations.OrderByDescending(r => prop.GetValue(r).ToString()).ThenBy(r => r.EndDate);
            }
            
            return _reservations.OrderBy(r => prop.GetValue(r).ToString()).ThenBy(r => r.EndDate);
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
