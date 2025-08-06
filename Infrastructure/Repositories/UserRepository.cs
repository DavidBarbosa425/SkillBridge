using Domain.Common;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Result<User>> AddAsync(User user)
        {
            await _applicationDbContext.Users.AddAsync(user);

            var saveResult = await _applicationDbContext.SaveChangesAsync();

            if(saveResult <= 0)
                return Result<User>.Failure("Erro ao criar usuário no dominio");

            return Result<User>.Ok(user);
        }
        public async Task<Result<User>> FindByIdAsync(Guid id)
        {
            var user = await _applicationDbContext.Users
                .Include(u => u.ItServiceProviders)
                .Include(u => u.Companies)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                return Result<User>.Failure("Usuário não encontrado.");

            return Result<User>.Ok(user);
        }
        public async Task<Result<User>> FindByEmailAsync(string email)
        {
            var user = await _applicationDbContext.Users
                .Include(u => u.ItServiceProviders)
                .Include(u => u.Companies)
                .FirstOrDefaultAsync(u => u.Email.Contains(email));

            if (user is null)
                return Result<User>.Failure("Usuário não encontrado.");

            return Result<User>.Ok(user);
        }
    }
}
