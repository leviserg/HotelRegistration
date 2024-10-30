using HotelRegistration.Commands;
using HotelRegistration.Models;
using HotelRegistration.Services;
using HotelRegistration.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelRegistration.ViewModels
{
    public class ReservationListViewModel : ViewModelBase
    {

        private readonly ObservableCollection<ReservationViewModel> _reservationsObservable;
        private readonly ReservationCacheStore _cache;

        public IEnumerable<ReservationViewModel> Reservations => _reservationsObservable;

        private bool isLoading;

        public bool IsLoading
        {
            get { return isLoading; }
            set {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set {
                errorMessage = value;
                OnPropertyChanged(nameof (ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));

            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);



        public ICommand? NavigateToMakeReservationPage { get; }
        public ICommand? LoadCommand { get; }

        public ReservationListViewModel(ReservationCacheStore cache, ViewModelNavigationService navigationService)
        {
            _reservationsObservable = new ObservableCollection<ReservationViewModel>();

            _cache = cache;

            LoadCommand = new LoadReservationsCommand(_cache, this);
            NavigateToMakeReservationPage = new NavigateCommand(navigationService);

            _cache.ReservationCreated += OnReservationMade;

        }

        public static ReservationListViewModel LoadViewModel(ReservationCacheStore cache, ViewModelNavigationService navigationService)
        {


            ReservationListViewModel viewModel = new ReservationListViewModel(cache, navigationService);
            viewModel.LoadCommand.Execute(null);
            return viewModel;
        }


        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservationsObservable.Clear();

            foreach (var reservation in reservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservationsObservable.Add(reservationViewModel);
            }

        }

        public override void Dispose()
        {
            _cache.ReservationCreated -= OnReservationMade;
            base.Dispose();
        }

        private void OnReservationMade(Reservation reservation)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
            _reservationsObservable.Add(reservationViewModel);
        }

    }
}
