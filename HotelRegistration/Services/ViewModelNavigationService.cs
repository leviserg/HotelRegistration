using HotelRegistration.Stores;
using HotelRegistration.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.Services
{
    public class ViewModelNavigationService
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _navigateTo;

        public ViewModelNavigationService(NavigationStore navigationStore, Func<ViewModelBase> navigateTo)
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
