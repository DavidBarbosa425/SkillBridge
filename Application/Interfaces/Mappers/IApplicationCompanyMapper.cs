using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.Mappers
{
    public interface IApplicationCompanyMapper
    {
        Company ToCompany(RegisterCompanyDto dto);
    }
}
