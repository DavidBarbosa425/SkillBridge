using API.Interfaces.Mappers;
using API.Models;
using Application.DTOs;

namespace API.Mappers
{
    public class ApiItServiceProviderMapper : IApiItServiceProviderMapper
    {
        public RegisterItServiceProviderDto ToRegisterItServiceProviderDto(RegisterItServiceProviderRequest request)
        {
            return new RegisterItServiceProviderDto
            {
                UserId = request.UserId,
                JobTitle = request.JobTitle,
            };
        }
    }
}
