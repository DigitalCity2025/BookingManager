using BookingManager.DAL.Enums;
using System.ComponentModel.DataAnnotations.Schema;
namespace BookingManager.DAL.Entities
{
    [Table("Booking")]
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BookingStatus Status { get; set; }
        public int Discount { get; set; }
        public decimal Price { get; set; }

        public int RoomId { get; set; }
        public int CustomerId { get; set; }

        public Room Room { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
    }
}
