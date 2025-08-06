using Application.DTOs;
using Application.Interfaces.Mappers;
using Domain.Entities;

namespace Application.Mappers
{
    public class ApplicationCompanyMapper : IApplicationCompanyMapper
    {
        public Company ToCompany(RegisterCompanyDto dto)
        {
            return new Company
            {
                Id = new Guid(),
                UserId = dto.UserId,
                Name = dto.Name,
                CNPJ = dto.CNPJ,
            };
        }
    }
}
