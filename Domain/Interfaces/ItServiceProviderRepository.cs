using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IItServiceProviderRepository
    {
        Task<Result<ItServiceProvider>> AddAsync(ItServiceProvider itServiceProvider);
    }
}
