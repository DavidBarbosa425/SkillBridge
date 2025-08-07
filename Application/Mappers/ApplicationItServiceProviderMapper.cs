using Application.DTOs;
using Application.Interfaces.Mappers;
using Domain.Entities;

namespace Application.Mappers
{
    public class ApplicationItServiceProviderMapper : IApplicationItServiceProviderMapper
    {
        public ItServiceProvider ToItServiceProvider(RegisterItServiceProviderDto dto)
        {
            return new ItServiceProvider
            {
                UserId = dto.UserId,
                JobTitle = dto.JobTitle
            };
        }
    }
}
