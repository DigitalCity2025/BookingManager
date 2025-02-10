using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManager.DAL.Entities
{
    [Table("Login")]
    [Index(nameof(Username), IsUnique = true)]
    public class Login
    {
        public int LoginId { get; set; }
        public string Username { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
        public virtual string Role { get; } = "Admin";
    }
}
