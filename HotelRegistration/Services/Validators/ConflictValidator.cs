using HotelRegistration.DbContexts;
using HotelRegistration.Models;
using HotelRegistration.Services.ReservationProviders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.Services.Validators
{
    public class ConflictValidator : IConflictValidator
    {
        private readonly ReservationDbContextFactory _dbContextFactory;

        public ConflictValidator(ReservationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<Reservation> HasConflictWith(Reservation reservation)
        {
            using (ReservationDbContext context = _dbContextFactory.CreateDbContext())
            {
                var dto = await context.Reservations
                    .Where(
                        r => 
                            r.RoomNumber == reservation.Room.RoomNumber
                            && r.FloorNumber == reservation.Room.FloorNumber
                            && r.VisitorName != reservation.VisitorName
                            && r.StartDate <= reservation.EndDate
                            && r.EndDate >= reservation.StartDate
                        )
                    .FirstOrDefaultAsync();

                if (dto == null) {
                    return null;
                }

                return ReservationProvider.ToReservation( dto );
            }
        }
    }
}
