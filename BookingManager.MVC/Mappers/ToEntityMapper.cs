using BookingManager.DAL.Entities;
using BookingManager.MVC.Models;

namespace BookingManager.MVC.Mappers
{
    public static class ToEntityMapper
    {
        public static Customer ToEntity(this CustomerCreateFormViewModel model)
        {
            return new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };
        }
    }
}
