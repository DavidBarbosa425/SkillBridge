using API.Interfaces.Mappers;
using API.Models;
using Application.DTOs;

namespace API.Mappers
{
    public class ApiCompanyMapper : IApiCompanyMapper
    {
        public RegisterCompanyDto ToRegisterCompanyDto(RegisterCompanyRequest request)
        {
            return new RegisterCompanyDto
            {
                UserId = request.UserId,
                Name = request.Name,
                CNPJ = request.CNPJ,
            };
        }
    }
}
