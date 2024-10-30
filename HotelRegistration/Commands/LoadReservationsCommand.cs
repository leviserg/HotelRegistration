using HotelRegistration.Exceptions;
using HotelRegistration.Models;
using HotelRegistration.Services;
using HotelRegistration.Stores;
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
        private readonly ReservationCacheStore _cache;
        private readonly ReservationListViewModel _viewModel;

        public LoadReservationsCommand(ReservationCacheStore cache, ReservationListViewModel viewModel)
        {
            _cache = cache;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.IsLoading = true;
            try
            {

                await _cache.Load();
                _viewModel.UpdateReservations(_cache.Reservations);

            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                _viewModel.ErrorMessage = "Failed to load reservations";
            }

            _viewModel.IsLoading = false;

        }

    }
}
