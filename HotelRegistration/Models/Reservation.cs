using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.Models
{
    public class Reservation
    {
        public Room Room { get; }
        public string VisitorName { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public int DaysReserved => (int) EndDate.Subtract(StartDate).TotalDays;


        public Reservation(Room room, string visitorName, DateTime startTime, DateTime endTime)
        {
            Room = room;
            StartDate = startTime;
            EndDate = endTime;
            VisitorName = visitorName;
        }

        public bool Conflicts(Reservation other)
        {
            return other.Room.Equals(Room)
                && other.VisitorName != VisitorName
                && other.StartDate <= EndDate
                &&  other.EndDate >= StartDate;            
        }


    }
}
