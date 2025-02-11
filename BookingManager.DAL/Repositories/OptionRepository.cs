using BookingManager.Application.Abstractions;
using BookingManager.DAL.Entities;

namespace BookingManager.DAL.Repositories
{
    public class OptionRepository 
        : CrudRepositoryBase<Option>, IOptionRepository
    {
        
    }
}
