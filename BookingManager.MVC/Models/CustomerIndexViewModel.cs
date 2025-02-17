namespace BookingManager.MVC.Models
{
    public record CustomerIndexViewModel(
        int Id, 
        string LastName, 
        string FirstName, 
        string Email, 
        int BookingsNb
    );
}
