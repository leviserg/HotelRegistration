using HotelRegistration.Services;
using HotelRegistration.ViewModels;

namespace HotelRegistration.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly ViewModelNavigationService<TViewModel> _navigationService;

        public NavigateCommand(ViewModelNavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
