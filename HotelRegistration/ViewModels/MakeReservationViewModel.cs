using HotelRegistration.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelRegistration.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase
    {
        
        private string? _visitorName;
        private int _floorNumber;
        private int _roomNumber;
        private DateTime? _startDate;
        private DateTime? _endDate;
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
                _endDate = value; OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value; OnPropertyChanged(nameof(EndDate));
            }
        }
        
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public MakeReservationViewModel()
        {
            SubmitCommand = new MakeReservationCommand();
        }
    }
}
