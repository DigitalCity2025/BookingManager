using System.ComponentModel.DataAnnotations;

namespace BookingManager.MVC.Models
{
    public class CustomerSearchFormViewModel
    {
        [MaxLength(50)]
        public string? Search { get; set; }

        public List<CustomerIndexViewModel> Results { get; set; } = [];
    }
}
