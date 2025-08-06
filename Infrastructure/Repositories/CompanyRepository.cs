using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CompanyRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Result<Company>> AddAsync(Company company)
        {
            await _applicationDbContext.Companies.AddAsync(company);

            var saveResult = await _applicationDbContext.SaveChangesAsync();

            if (saveResult <= 0)
                return Result<Company>.Failure("Erro ao cadastrar empresa");

            return Result<Company>.Ok(company);
        }
    }
}
