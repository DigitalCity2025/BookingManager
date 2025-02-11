using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManager.DAL.Entities
{
    [Table("Room")]
    [Index(nameof(ImageUrl), IsUnique = true)]
    public class Room
    {
        public int RoomId { get; set; }
        public string Number { get; set; } = null!;
        public int Floor { get; set; }
        public int Surface { get; set; }
        [Column(name: "image", TypeName = "varchar(max)")]
        public string ImageUrl { get; set; } = null!;
        public int MaxCapacity { get; set; }
        public decimal Price { get; set; }
        public List<Option> Options { get; set; } = null!;
        public List<Booking> Bookings { get; set; } = null!;
    }
}
