using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookingManager.MVC.Models
{
    public class CustomerCreateFormViewModel
    {
        [DisplayName("Nom")]
        [Required(ErrorMessage = "Champs requis")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = null!;

        [DisplayName("Prénom")]
        [Required(ErrorMessage = "Champs requis")]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [DisplayName("Email")]
        [Required(ErrorMessage = "Champs requis")]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [DisplayName("N° Tel")]
        [StringLength(50)]
        public string? PhoneNumber { get; set; }
    }
}
