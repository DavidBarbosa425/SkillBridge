using API.Models;
using Application.DTOs;

namespace API.Interfaces.Mappers
{
    public interface IApiCompanyMapper
    {
        RegisterCompanyDto ToRegisterCompanyDto(RegisterCompanyRequest request);
    }
}
