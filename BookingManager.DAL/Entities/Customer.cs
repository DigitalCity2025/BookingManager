using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManager.DAL.Entities
{
    [Table("Customer")]
    [Index(nameof(Email), IsUnique = true)]
    public class Customer: Login
    {
        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; } = null!;
        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; } = null!;
        [Column(TypeName = "varchar(255)")]
        public string Email { get; set; } = null!;
        [Column(TypeName = "varchar(50)")]
        public string? PhoneNumber { get; set; }
        public override string Role => "Customer";
        public List<Booking> Bookings { get; set; } = null!;
    }
}
