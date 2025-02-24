using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookingManager.MVC.Models
{
    public class CustomerBaseFormViewModel
    {
        [DisplayName("Nom")]
        [Required(ErrorMessage = "Champs requis")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = null!;

        [DisplayName("Prénom")]
        [Required(ErrorMessage = "Champs requis")]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [DisplayName("N° Tel")]
        [StringLength(50)]
        public string? PhoneNumber { get; set; }
    }
}
