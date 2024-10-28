using HotelRegistration.DbContexts;
using HotelRegistration.DTOs;
using HotelRegistration.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.Services.ReservationCreators
{
    public class ReservationCreator : IReservationCreator
    {
        private readonly ReservationDbContextFactory _dbContextFactory;

        public ReservationCreator(ReservationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<int> CreateReservationAsync(Reservation reservation)
        {
            using (ReservationDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDto reservationDTO = ToReservationDto(reservation);
                context.Reservations.Add(reservationDTO);
                await context.SaveChangesAsync();
                return reservationDTO.ReservationId;
            }
        }

        private ReservationDto ToReservationDto(Reservation reservation)
        {
            return new ReservationDto()
            {
                FloorNumber = reservation.Room?.FloorNumber ?? 0,
                RoomNumber = reservation.Room?.RoomNumber ?? 0,
                VisitorName = reservation.VisitorName,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate
            };
        }
    }
}
