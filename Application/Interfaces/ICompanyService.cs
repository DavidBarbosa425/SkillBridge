using Application.DTOs;
using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICompanyService
    {
        Task<Result> RegisterAsync(RegisterCompanyDto registerCompanyDto);
    }
}
