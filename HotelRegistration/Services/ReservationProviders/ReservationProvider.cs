using HotelRegistration.DbContexts;
using HotelRegistration.DTOs;
using HotelRegistration.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.Services.ReservationProviders
{
    public class ReservationProvider : IReservationProvider
    {

        private readonly ReservationDbContextFactory _dbContextFactory;

        public ReservationProvider(ReservationDbContextFactory dbContextFactory)
        {
             _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            using (ReservationDbContext context = _dbContextFactory.CreateDbContext())
            {
                var reservationDTOs = await context.Reservations.ToListAsync();
                return reservationDTOs.Select(r => ToReservation(r));
            }
        }

        public static Reservation ToReservation(ReservationDto r)
        {
            return new Reservation(
                                    new Room(r.FloorNumber, r.RoomNumber),
                                    r.VisitorName,
                                    r.StartDate,
                                    r.EndDate
                                );
        }
    }
}
