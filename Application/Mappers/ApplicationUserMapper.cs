using Application.DTOs;
using Application.Interfaces.Mappers;
using Domain.Entities;

public class ApplicationUserMapper : IApplicationUserMapper
{
    public User ToUser(RegisterUserDto dto)
    {
        return new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = dto.Password
        };
    }
}