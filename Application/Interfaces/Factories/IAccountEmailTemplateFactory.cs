namespace Application.Interfaces.Factories
{
    public interface IAccountEmailTemplateFactory
    {
        string GenerateConfirmationEmailHtml(string userName, string confirmationLink);
        string GeneratePasswordResetEmailHtml(string userName, string link);
    }
}
