using HotelRegistration.Models;
using HotelRegistration.Stores;
using HotelRegistration.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _navigateTo;

        public NavigateCommand(NavigationStore navigationStore, Func<ViewModelBase> navigateTo)
        {
            _navigationStore = navigationStore;
            _navigateTo = navigateTo;
        }



        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _navigateTo();
        }
    }
}
