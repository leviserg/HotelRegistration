using HotelRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.Stores
{
    public class ReservationCacheStore
    {
        private readonly List<Reservation> _reservations;
        private readonly Hotel _hotel;
        private Lazy<Task> _initializeLazyLoad; // threadsafe

        public IEnumerable<Reservation> Reservations => _reservations;
        public event Action<Reservation> ReservationCreated;

        public ReservationCacheStore(Hotel hotel)
        {
             _reservations = new List<Reservation>();
            // _initializeLazyLoad = new Lazy<Task>(async () => await Initialize()); // method factory - same as simple creator
            _initializeLazyLoad = new Lazy<Task>(Initialize); // simple creator
            _hotel = hotel;
        }

        public async Task<int> MakeReservation(Reservation reservation)
        {
            var reservationId = await _hotel.MakeReservation(reservation);
            _reservations.Add(reservation);

            OnReservationMade(reservation);

            return reservationId;
        }

        private void OnReservationMade(Reservation reservation)
        {
            ReservationCreated?.Invoke(reservation);
        }

        public async Task Load()
        {
            try
            {
                await _initializeLazyLoad.Value;
            }
            catch (Exception)
            {
                _initializeLazyLoad = new Lazy<Task>(Initialize);
                throw;
            }

        }

        private async Task Initialize()
        {
            IEnumerable<Reservation> dBreservations = await _hotel.GetReservations(); // ininitalize once due to lazy load value
            _reservations.Clear();
            _reservations.AddRange(dBreservations);
        }
    }
}
