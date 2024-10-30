using HotelRegistration.Commands;
using HotelRegistration.Services;
using HotelRegistration.Stores;
using System.Collections;
using System.ComponentModel;
using System.Windows.Input;

namespace HotelRegistration.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
    {

        #region Properties

        private string? _visitorName;
        private int _floorNumber;
        private int _roomNumber;
        private DateTime? _startDate = DateTime.Now;
        private DateTime? _endDate = DateTime.Now.AddDays(3);

        public string VisitorName
        {
            get => _visitorName ?? string.Empty;
            set
            {
                _visitorName = value;
                OnPropertyChanged(nameof(VisitorName));
            }
        }

        public int FloorNumber
        {
            get { return _floorNumber; }
            set
            {
                _floorNumber = value;
                OnPropertyChanged(nameof(FloorNumber));
            }
        }

        public int RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                _roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }

        public DateTime? StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value; OnPropertyChanged(nameof(StartDate));
                ClearErrors(nameof(EndDate));
                ClearErrors(nameof(StartDate));

                if (EndDate < StartDate)
                {
                    AddError(nameof(StartDate), $"The start date of {StartDate?.ToString("dd.MM.yyyy")} can't be greater than end date of {EndDate?.ToString("dd.MM.yyyy")}");
                }
            }
        }

        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {

                _endDate = value;

                OnPropertyChanged(nameof(EndDate));

                ClearErrors(nameof(EndDate));
                ClearErrors(nameof(StartDate));

                if (EndDate < StartDate)
                {
                    AddError(nameof(EndDate), $"The end date of {EndDate?.ToString("dd.MM.yyyy")} can't be less than start date of {StartDate?.ToString("dd.MM.yyyy")}");
                }


            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        #endregion

        public MakeReservationViewModel(ReservationCacheStore cache, ViewModelNavigationService<ReservationListViewModel> navigationService)
        {
            SubmitCommand = new MakeReservationCommand(cache, this, navigationService);
            CancelCommand = new NavigateCommand<ReservationListViewModel>(navigationService);
            _propertyNameToErrorDictionary = new Dictionary<string, List<string>>();
        }

        #region NotifyErrors

        private readonly Dictionary<string, List<String>> _propertyNameToErrorDictionary;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool HasErrors => _propertyNameToErrorDictionary.Any();

        public IEnumerable GetErrors(string propertyName)
        {
            return _propertyNameToErrorDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void AddError(string propertyName, string message) {

            if (!_propertyNameToErrorDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorDictionary.Add(propertyName, new List<string>(1));
            }
            _propertyNameToErrorDictionary[propertyName].Add(message);
            
            OnErrorsChanged(propertyName);
        }
        private void ClearErrors(string propertyName) {
            _propertyNameToErrorDictionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion
    }
}
