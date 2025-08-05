using Application.DTOs;
using Application.Interfaces.Mappers;
using Domain.Entities;

internal class ApplicationUserMapper : IApplicationUserMapper
{
    public User ToUser(RegisterUserDto dto)
    {
        return new User
        {
            Name = dto.Name,
            FullName = dto.FullName,
            PreferredName = dto.PreferredName,
            Email = dto.Email
        };
    }

    public User ToUser(UserDto dto)
    {
        return new User
        {
            Id = dto.Id,
            Name = dto.Name,
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
            Name = user.Name,
            Email = user.Email,
            IdentityUserId = user.IdentityUserId
        };
    }

    public UserRegistered ToUserRegistered(User user)
    {
        return new UserRegistered
        {
            Name = user.Name,
            Email = user.Email,
            IdentityUserId = user.IdentityUserId
        };
    }

    public UserForgotPassword ToUserForgotPassword(User user)
    {
        return new UserForgotPassword
        {
            Name = user.Name,
            Email = user.Email,
            IdentityUserId = user.IdentityUserId
        };
    }

    public User ToCreateUser(string identityUser, User user)
    {
        return new User
        {
            Id = new Guid(),
            Name = user.Name,
            FullName = user.FullName,
            PreferredName = user.PreferredName,
            Email = user.Email,
            IdentityUserId = identityUser
        };
    }
}