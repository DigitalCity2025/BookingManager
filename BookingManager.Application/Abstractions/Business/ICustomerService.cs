using BookingManager.DAL.Entities;

namespace BookingManager.Application.Abstractions.Business
{
    public interface ICustomerService
    {
        public Customer Create(Customer c);

        public void Delete(int id);
        IEnumerable<Customer> FindByKeyword(string? search);
    }
}
