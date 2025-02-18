using BookingManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManager.Application.Abstractions
{
    public interface ICustomerRepository : ICrudRepository<Customer>
    {
        List<Customer> FindByKeyword(string? keyword);
        List<Customer> GetByYear(int year);
        Customer? GetByEmail(string email);
    }
}
