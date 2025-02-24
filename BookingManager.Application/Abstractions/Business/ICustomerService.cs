using BookingManager.DAL.Entities;

namespace BookingManager.Application.Abstractions.Business
{
    public interface ICustomerService
    {
        public Customer Create(Customer c);

        public void Delete(int id);
        IEnumerable<Customer> FindByKeyword(string? search);
        Customer? GetById(int id);
        void Update(int id, string lastName, string firstName, string? password, string? phoneNumber);
    }
}
