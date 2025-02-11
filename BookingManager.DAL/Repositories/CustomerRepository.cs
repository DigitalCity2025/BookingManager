using BookingManager.Application.Abstractions;
using BookingManager.DAL.Entities;

namespace BookingManager.DAL.Repositories
{
    public class CustomerRepository 
        : CrudRepositoryBase<Customer>, ICustomerRepository
    {
        public Customer? GetByEmail(string email)
        {
            using HotelContext ctx = new HotelContext();
            return ctx.Customers.Where(c => c.Email == email).FirstOrDefault();
        }

        public List<Customer> GetByYear(int year)
        {
            using HotelContext ctx = new HotelContext();
            return ctx.Customers
                .Where(c => c.Bookings.Any(b => b.BookingDate.Year == year)).ToList();
        }
    }
}
