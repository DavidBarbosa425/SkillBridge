namespace Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendRegistrationConfirmationAsync(string name, string email);
    }
}
