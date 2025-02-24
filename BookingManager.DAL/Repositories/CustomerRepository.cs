using BookingManager.Application.Abstractions.Repositories;
using BookingManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingManager.DAL.Repositories
{
    public class CustomerRepository(HotelContext ctx) 
        : CrudRepositoryBase<Customer>(ctx), ICustomerRepository
    {

        public override List<Customer> GetAll()
        {
            return ctx.Customers.Include(c => c.Bookings)
                .ToList();
        }

        public List<Customer> FindByKeyword(string? keyword)
        {
            return ctx.Customers.Include(c => c.Bookings)
                .Where(c => 
                    keyword == null
                    || c.LastName.Contains(keyword) 
                    || c.FirstName.Contains(keyword) 
                    || c.Email.Contains(keyword)
                )
                .ToList();
        }

        public Customer? GetByEmail(string email)
        {
            return ctx.Customers.Where(c => c.Email == email).FirstOrDefault();
        }

        public List<Customer> GetByYear(int year)
        {
            return ctx.Customers
                .Where(c => c.Bookings.Any(b => b.BookingDate.Year == year)).ToList();
        }

        public int CountByUsername(string prefix)
        {
            return ctx.Customers.Count(c => c.Username.StartsWith(prefix));
        }

        public Customer? FindOneByUsernameOrEmail(string usernameOrEmail)
        {
            return ctx.Customers.SingleOrDefault(c => c.Username == usernameOrEmail || c.Email == usernameOrEmail);
        }
    }
}
