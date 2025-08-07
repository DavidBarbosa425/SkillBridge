using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.Mappers
{
    public interface IApplicationItServiceProviderMapper
    {
        ItServiceProvider ToItServiceProvider(RegisterItServiceProviderDto dto);
    }
}
