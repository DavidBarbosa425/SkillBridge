using Application.DTOs;
using Domain.Entities;

public static class ApplicationUserMapper
{
    public static User ToUser(RegisterUserDto dto)
    {
        return new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = dto.Password
        };
    }

}