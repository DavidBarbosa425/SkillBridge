using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.Mappers
{
    public interface IApplicationUserMapper
    {
        User ToUser(RegisterUserDto dto);
        UserDto ToUserDto(RegisterUserDto dto);
    }
}
