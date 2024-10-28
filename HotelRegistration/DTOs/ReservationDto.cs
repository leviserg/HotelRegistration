using HotelRegistration.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace HotelRegistration.DTOs
{
    public class ReservationDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int ReservationId { get; set; }
        public int FloorNumber { get; set; }
        public int RoomNumber { get; set; }
        public string VisitorName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}