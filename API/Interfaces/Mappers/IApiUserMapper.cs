using API.Models;
using Application.DTOs;

namespace API.Interfaces.Mappers
{
    public interface IApiUserMapper
    {
        ConfirmEmailDto ToConfirmEmailDto(ConfirmEmailRequest confirmEmailRequest);
    }
}
