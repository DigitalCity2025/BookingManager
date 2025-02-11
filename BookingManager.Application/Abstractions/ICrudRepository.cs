using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManager.Application.Abstractions
{
    public interface ICrudRepository<T> where T : class
    {
        List<T> GetAll();
        T? GetById(int id);
        T Add(T entity);
        T Update(T entity);
        void Remove(T entity);
    }
}
