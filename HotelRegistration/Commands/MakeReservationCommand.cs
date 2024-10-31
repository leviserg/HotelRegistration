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
            return _viewModel.CanCreateReservation && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.SubmitErrorMessage = string.Empty;
            _viewModel.IsSubmitting = true;

            try
            {
                Reservation reservation = new Reservation(_viewModel);

                var reservationId = await _cache.MakeReservation(reservation);

                MessageBox.Show($"Room successfully reserved,\n Reservation Id = {reservationId}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                _navigationService.Navigate();

            }
            catch (ReservationConflictException ex)
            {
                // MessageBox.Show("This room is already taken","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _viewModel.SubmitErrorMessage = "This room is already taken";
            }
            catch (InvalidDateRangeException ex)
            {
                // MessageBox.Show("This room is already taken","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _viewModel.SubmitErrorMessage = "Start date must be before end date";
            }
            catch (DbUpdateException ex)
            {
                //MessageBox.Show("Failed to save record to Db", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _viewModel.SubmitErrorMessage = "Failed to save record to Db";
            }
            catch (Exception ex) {
                MessageBox.Show($"Program execution failed:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }

            _viewModel.IsSubmitting = false;

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
