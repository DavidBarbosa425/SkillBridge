﻿using Application.DTOs;
using Application.Interfaces.Mappers;
using Domain.Entities;

internal class ApplicationUserMapper : IApplicationUserMapper
{
    public User ToUser(RegisterUserDto dto)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
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
            Email = user.Email
        };
    }
}