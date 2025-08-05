using Application.DTOs;
using Application.Interfaces.Mappers;
using Domain.Entities;

internal class ApplicationUserMapper : IApplicationUserMapper
{
    public User ToUser(RegisterUserDto dto)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FullName = dto.Name,
            Email = dto.Email
        };
    }

    public User ToUser(UserDto dto)
    {
        return new User
        {
            Id = dto.Id,
            FullName = dto.Name,
            Email = dto.Email,
        };
    }
    public User ToUser(LoginDto dto)
    {
        return new User
        {
            Email = dto.Email
        };
    }

    public UserDto ToUserDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.FullName,
            Email = user.Email,
            IdentityUserId = user.IdentityUserId
        };
    }

    public UserRegistered ToUserRegistered(User user)
    {
        return new UserRegistered
        {
            Name = user.FullName,
            Email = user.Email,
            IdentityUserId = user.IdentityUserId
        };
    }

    public UserForgotPassword ToUserForgotPassword(User user)
    {
        return new UserForgotPassword
        {
            Name = user.FullName,
            Email = user.Email,
            IdentityUserId = user.IdentityUserId
        };
    }

    public User ToCreateUser(string identityUser, User user)
    {
        return new User
        {
            FullName = user.FullName,
            Email = user.Email,
            IdentityUserId = identityUser
        };
    }
}