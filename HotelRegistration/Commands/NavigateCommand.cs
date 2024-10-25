using HotelRegistration.Models;
using HotelRegistration.Services;
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
        private readonly ViewModelNavigationService _navigationService;

        public NavigateCommand(ViewModelNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
