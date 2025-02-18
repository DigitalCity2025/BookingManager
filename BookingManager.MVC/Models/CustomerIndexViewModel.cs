using System.ComponentModel;

namespace BookingManager.MVC.Models
{
    public record CustomerIndexViewModel(
        int Id, 
        string LastName, 
        string FirstName, 
        string Email, 
        int BookingsNb
    )
    {
        [DisplayName("ID")]
        public int Id { get; } = Id;
        [DisplayName("Nom")]
        public string LastName { get; } = LastName;
        [DisplayName("Prénom")]
        public string FirstName { get; } = FirstName;
        [DisplayName("Email")]
        public string Email { get; } = Email;
        [DisplayName("Nombre de réservations")]
        public int BookingsNb { get; } = BookingsNb;
    }
}
