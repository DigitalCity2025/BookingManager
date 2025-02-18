using BookingManager.Application.Abstractions;
using BookingManager.DAL.Entities;

namespace BookingManager.MVC
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public List<Customer> FindByKeyword(string? keyword)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAll()
        {
            return
            [
                new Customer { 
                    LoginId  = 42,
                    LastName = "Foo",
                    FirstName = "Bar",
                    Email = "Bar",
                    Bookings = []
                }
            ];
        }

        public Customer? GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Customer? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetByYear(int year)
        {
            throw new NotImplementedException();
        }

        public void Remove(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Customer Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
