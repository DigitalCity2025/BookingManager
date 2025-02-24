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

        public static CustomerEditFormViewModel ToCustomerEditForm(this Customer entity)
        {
            return new CustomerEditFormViewModel
            {
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                PhoneNumber = entity.PhoneNumber,
            };
        }
    }
}
