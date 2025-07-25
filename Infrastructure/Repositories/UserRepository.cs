using Domain.Common;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public Result<User> AddAsync(User user)
        {
            _applicationDbContext.DomainUsers.Add(user);

            var saveResult = _applicationDbContext.SaveChanges();

            if(saveResult <= 0)
                return Result<User>.Failure("Erro ao criar usuário no dominio");

            return Result<User>.Ok(user);
        }
    }
}
