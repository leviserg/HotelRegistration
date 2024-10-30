using HotelRegistration.Exceptions;
using HotelRegistration.Models;
using HotelRegistration.Services;
using HotelRegistration.Stores;
using HotelRegistration.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace HotelRegistration.Commands
{
    public class MakeReservationCommand : AsyncCommandBase
    {
        private readonly ReservationCacheStore _cache;
        private readonly MakeReservationViewModel _viewModel;
        private readonly ViewModelNavigationService<ReservationListViewModel> _navigationService;

        public MakeReservationCommand(ReservationCacheStore cache,
            MakeReservationViewModel viewModel,
            ViewModelNavigationService<ReservationListViewModel> navigationService)
        {
            _cache = cache;
            _viewModel = viewModel;
            _navigationService = navigationService;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.VisitorName)
                && _viewModel.FloorNumber > 0
                && _viewModel.RoomNumber > 0
                && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {

            try
            {
                Reservation reservation = new Reservation(_viewModel);

                var reservationId = await _cache.MakeReservation(reservation);

                MessageBox.Show($"Room successfully reserved,\n Reservation Id = {reservationId}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                _navigationService.Navigate();

            }
            catch (ReservationConflictException ex)
            {
                MessageBox.Show("This room is already taken","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Failed to save record to Db", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                MessageBox.Show("Failed to make reservation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }

        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(MakeReservationViewModel.VisitorName)
                || e.PropertyName == nameof(MakeReservationViewModel.FloorNumber)
                || e.PropertyName == nameof(MakeReservationViewModel.RoomNumber)
                )
            {
                OnCanExecutedChanged();
            }
        }
    }
}
