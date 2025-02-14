using System.ComponentModel.DataAnnotations;

namespace BookingManager.MVC.Models
{
    public class ContactFormViewModel
    {
        [Required(ErrorMessage = "Ce champs est requis")]
        [MaxLength(50, ErrorMessage = "C'est trop long")]
        public string Sujet { get; set; } = null!;

        [Required(ErrorMessage = "Ce champs est requis")]
        [MinLength(10, ErrorMessage = "C'est trop court")]
        public string Message { get; set; } = null!;
    }
}
