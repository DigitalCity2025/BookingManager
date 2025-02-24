using System.ComponentModel.DataAnnotations;

namespace BookingManager.MVC.Models
{
    public class CustomerEditFormViewModel: CustomerBaseFormViewModel
    {
        [MinLength(8)]
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;""'<>,.?|`~-]).*")]
        [DataType(DataType.Password)]
        public string? Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; } = null!;
    }
}
