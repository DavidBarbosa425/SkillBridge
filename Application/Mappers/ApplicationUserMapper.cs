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

    public static UserDto ToUserDto(RegisterUserDto dto)
    {
        return new UserDto
        {
            Name = dto.Name,
            Email = dto.Email
        };
    }

    public static SendEmail ToSendEmail(SendEmailDto dto)
    {
        return new SendEmail
        {
            Email = dto.Email,
            Subject = dto.Subject,
            Body = dto.Body
        };
    }

}