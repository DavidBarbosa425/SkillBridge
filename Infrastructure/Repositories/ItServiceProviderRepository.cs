using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ItServiceProviderRepository : IItServiceProviderRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ItServiceProviderRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Result<ItServiceProvider>> AddAsync(ItServiceProvider itServiceProvider)
        {
            await _applicationDbContext.ItServiceProviders.AddAsync(itServiceProvider);

            var save = await _applicationDbContext.SaveChangesAsync();

            if (save < 0)
                return Result<ItServiceProvider>.Failure("It Service Provider registered successfully.");


            return Result<ItServiceProvider>.Ok(itServiceProvider, "It Service Provider registered successfully.");
        }
    }
}
