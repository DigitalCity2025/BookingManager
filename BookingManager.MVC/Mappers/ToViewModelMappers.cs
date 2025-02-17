using BookingManager.DAL.Entities;
using BookingManager.MVC.Models;

namespace BookingManager.MVC.Mappers
{
    public static class ToViewModelMappers
    {
        public static CustomerIndexViewModel 
            ToCustomerIndex(this Customer entity)
        {
            return new CustomerIndexViewModel(
                entity.LoginId,
                entity.LastName,
                entity.FirstName,
                entity.Email,
                entity.Bookings.Count()
            );
        }
    }
}
