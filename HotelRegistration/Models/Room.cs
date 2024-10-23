using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRegistration.Models
{
    public class Room
    {
        public int FloorNumber { get; }
        public int RoomNumber { get; }

        public Room(int floorNumber, int roomNumber)
        {
            FloorNumber = floorNumber;
            RoomNumber = roomNumber;
        }

        public override bool Equals(object obj)
        {
            return obj is Room room && FloorNumber == room.FloorNumber && RoomNumber == room.RoomNumber;
        }

        public override int GetHashCode() { 
            return HashCode.Combine(FloorNumber, RoomNumber);
        }

        public override string ToString() {
            return $"{FloorNumber}0{RoomNumber}";
        }

    }
}
