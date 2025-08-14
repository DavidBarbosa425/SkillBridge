using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.Mappers
{
    public interface IApplicationUserMapper
    {
        User ToUser(RegisterUserDto dto);
        User ToUser(UserDto dto);
        User ToUser(LoginRequestDto dto);
        User ToCreateUser(string identityId, User user);
        UserDto ToUserDto(User user);
        UserRegistered ToUserRegistered(User user);
        UserForgotPassword ToUserForgotPassword(User user);

    }
}
