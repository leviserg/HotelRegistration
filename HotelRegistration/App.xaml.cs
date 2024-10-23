using HotelRegistration.Exceptions;
using HotelRegistration.Models;
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
            try
            {
                Hotel hotel = new Hotel("Singleton Suites");

                hotel.MakeReservation(new Reservation(
                        new Room(1, 3),
                        "John Doe",
                        new DateTime(2024, 10, 20),
                        new DateTime(2024, 10, 25)
                    ));

                hotel.MakeReservation(new Reservation(
                    new Room(1, 3),
                    "Jane Eyre",
                    new DateTime(2024, 10, 15),
                    new DateTime(2024, 10, 22)
                ));

                IEnumerable<Reservation> selectedReservations = hotel.GetReservationsByVisitor(new Visitor("John Doe"));
            }
            catch (ReservationConflictException ex) { 
                Debug.WriteLine(ex.Message);
            }








            base.OnStartup(e);
        }
    }

}
