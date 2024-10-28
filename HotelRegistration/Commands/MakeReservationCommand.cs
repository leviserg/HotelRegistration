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
    public class MakeReservationCommand : AsyncCommandBase
    {
        private readonly Hotel _hotel;
        private readonly MakeReservationViewModel _viewModel;
        private readonly ViewModelNavigationService _navigationService;

        public MakeReservationCommand(Hotel hotel, MakeReservationViewModel viewModel, ViewModelNavigationService navigationService)
        {
            _hotel = hotel;
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
                var reservationId = await _hotel.MakeReservation(reservation);

                MessageBox.Show($"Room successfully reserved,\n Reservation Id = {reservationId}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                _navigationService.Navigate();

            }
            catch (ReservationConflictException ex)
            {
                MessageBox.Show("This room is already taken","Error", MessageBoxButton.OK, MessageBoxImage.Error);   
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
