namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        bool HasActiveTransaction { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
