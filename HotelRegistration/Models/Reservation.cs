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
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        public int DaysReserved => (int) EndTime.Subtract(StartTime).TotalDays;


        public Reservation(Room room, string visitorName, DateTime startTime, DateTime endTime)
        {
            Room = room;
            StartTime = startTime;
            EndTime = endTime;
            VisitorName = visitorName;
        }

        public bool Conflicts(Reservation other)
        {
            return other.Room.Equals(Room)
                && other.VisitorName != VisitorName
                && other.StartTime <= EndTime
                &&  other.EndTime >= StartTime;            
        }


    }
}
