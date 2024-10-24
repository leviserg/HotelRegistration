using HotelRegistration.Exceptions;
using HotelRegistration.Models;
using HotelRegistration.ViewModels;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;

namespace HotelRegistration
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel()
            };

            MainWindow.Show();
            base.OnStartup(e);
        }
    }

}
