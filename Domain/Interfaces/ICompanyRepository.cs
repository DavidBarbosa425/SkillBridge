using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Result<Company>> AddAsync(Company company);
    }
}
