using Application.DTOs;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IItServiceProviderService
    {
        Task<Result> RegisterAsync(RegisterItServiceProviderDto dto);
    }
}
