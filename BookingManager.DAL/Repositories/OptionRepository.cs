using BookingManager.Application.Abstractions.Repositories;
using BookingManager.DAL.Entities;

namespace BookingManager.DAL.Repositories
{
    public class OptionRepository 
        : CrudRepositoryBase<Option>, IOptionRepository
    {
        
    }
}
