using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookingManager.MVC.Models
{
    public class CustomerCreateFormViewModel : CustomerBaseFormViewModel
    {

        [DisplayName("Email")]
        [Required(ErrorMessage = "Champs requis")]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
