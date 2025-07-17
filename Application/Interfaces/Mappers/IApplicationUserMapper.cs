using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.Mappers
{
    public interface IApplicationUserMapper
    {
        User ToUser(RegisterUserDto dto);
        User ToUser(UserDto dto);
        User ToUser(LoginDto dto);
        UserDto ToUserDto(User user);

    }
}
