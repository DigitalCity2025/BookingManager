using BookingManager.Application.Abstractions;
using BookingManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingManager.DAL.Repositories
{
    public class CustomerRepository 
        : CrudRepositoryBase<Customer>, ICustomerRepository
    {

        public override List<Customer> GetAll()
        {
            using HotelContext ctx = new HotelContext();
            return ctx.Customers.Include(c => c.Bookings)
                .ToList();
        }

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
