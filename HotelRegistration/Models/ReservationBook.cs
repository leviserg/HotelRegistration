using HotelRegistration.Exceptions;
using HotelRegistration.Services.ReservationCreators;
using HotelRegistration.Services.ReservationProviders;
using HotelRegistration.Services.Validators;
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
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IConflictValidator _conflictValidator;
        private const string defaultSortKey = nameof(Room);

        public ReservationBook(
            IReservationProvider reservationProvider,
            IReservationCreator reservationCreator,
            IConflictValidator conflictValidator
            )
        {
            _reservationProvider = reservationProvider;
            _reservationCreator = reservationCreator;
            _conflictValidator = conflictValidator;
        }

        public async Task<IEnumerable<Reservation>> GetReservations(string sortKey = defaultSortKey, bool sortDesc = false)
        {
            
            PropertyInfo prop = typeof(Reservation).GetProperty(sortKey);
            if (prop == null)
                prop = typeof(Reservation).GetProperty(defaultSortKey);

            var reservations = await _reservationProvider.GetReservationsAsync();

            if (sortDesc) {
                return reservations.OrderByDescending(r => prop.GetValue(r).ToString()).ThenBy(r => r.EndDate);
            }
            
            return reservations.OrderBy(r => prop.GetValue(r).ToString()).ThenBy(r => r.EndDate);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByVisitor(Visitor visitor) {
            var reservations = await _reservationProvider.GetReservationsAsync();
            return reservations.Where(r => r.VisitorName == visitor.Name);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByRoom(Room room)
        {
            var reservations = await _reservationProvider.GetReservationsAsync();
            return reservations.Where(r => r.Room.Equals(room));
        }

        public async Task<int> AddReservation(Reservation reservation) {

            var existingReservation = await _conflictValidator.HasConflictWith(reservation);

            if (existingReservation is not null) {
                throw new ReservationConflictException(existingReservation, reservation);
            }
            return await _reservationCreator.CreateReservationAsync(reservation);
        }
    }
}
