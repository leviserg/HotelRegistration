using HotelRegistration.Exceptions;
using HotelRegistration.Models;
using HotelRegistration.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelRegistration.Commands
{
    public class MakeReservationCommand : CommandBase
    {
        private readonly Hotel _hotel;
        private readonly MakeReservationViewModel _viewModel;

        public MakeReservationCommand(Hotel hotel, MakeReservationViewModel viewModel)
        {
            _hotel = hotel;
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.VisitorName)
                && _viewModel.FloorNumber > 0
                && _viewModel.RoomNumber > 0
                && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {

            try
            {
                Reservation reservation = new Reservation(_viewModel);
                _hotel.MakeReservation(reservation);

                MessageBox.Show("Room successfully reserved", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (ReservationConflictException ex)
            {
                MessageBox.Show("This room is already taken","Error", MessageBoxButton.OK, MessageBoxImage.Error);   
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
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
