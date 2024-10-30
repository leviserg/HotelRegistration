using HotelRegistration.Stores;
using HotelRegistration.ViewModels;

namespace HotelRegistration.Services
{
    public class ViewModelNavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _navigateTo;

        public ViewModelNavigationService(NavigationStore navigationStore, Func<TViewModel> navigateTo)
        {
            _navigationStore = navigationStore;
            _navigateTo = navigateTo;
        }
        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _navigateTo();
        }
    }
}
