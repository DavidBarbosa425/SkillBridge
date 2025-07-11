﻿using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Result<User>> AddAsync(User user);
    }
}
