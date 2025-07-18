using API.Interfaces.Mappers;
using API.Models;
using Application.DTOs;

namespace API.Mappers
{
    public class ApiUserMapper : IApiUserMapper
    {
        public ConfirmEmailDto ToConfirmEmailDto(ConfirmEmailRequest dto)
        {
            return new ConfirmEmailDto
            {
              UserId = dto.UserId,
              Token = dto.Token
            };
        }
    }
}
