namespace Application.Interfaces.Factories
{
    public interface IEmailTemplateFactory
    {
        string GenerateConfirmationEmailHtml(string userName, string confirmationLink);
    }
}
