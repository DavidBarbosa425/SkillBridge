namespace Domain.Interfaces
{
    public interface IEmailService
    {
        void SendRegistrationConfirmationAsync(string name, string email);
    }
}
