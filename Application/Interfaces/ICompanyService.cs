using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICompanyService
    {
        Task<bool> RegisterAsync(RegisterCompanyDto registerCompanyDto);
    }
}
