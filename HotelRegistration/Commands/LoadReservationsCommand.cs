using HotelRegistration.Exceptions;
using HotelRegistration.Models;
using HotelRegistration.Services;
using HotelRegistration.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace HotelRegistration.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly Hotel _hotel;
        private readonly ReservationListViewModel _viewModel;

        public LoadReservationsCommand(Hotel hotel, ReservationListViewModel viewModel)
        {
            _hotel = hotel;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                IEnumerable<Reservation> reservations = await _hotel.GetReservations();
                _viewModel.UpdateReservations(reservations);
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                MessageBox.Show("Failed to load reservations", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }

        }

    }
}
