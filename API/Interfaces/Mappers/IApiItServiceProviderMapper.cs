using API.Models;
using Application.DTOs;

namespace API.Interfaces.Mappers
{
    public interface IApiItServiceProviderMapper
    {
        RegisterItServiceProviderDto ToRegisterItServiceProviderDto(RegisterItServiceProviderRequest request);
    }
}
