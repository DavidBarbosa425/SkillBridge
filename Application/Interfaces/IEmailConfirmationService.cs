using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEmailConfirmationService
    {
        Task<SendEmail> GenerateEmailConfirmation(User user);
    }
}
