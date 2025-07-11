using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces.Data
{
    public interface IApplicationDbContext
    {
        DbSet<EmailConfirmationToken> EmailConfirmationTokens { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
