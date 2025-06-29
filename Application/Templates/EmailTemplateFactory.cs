namespace Application.Templates
{
    public static class EmailTemplateFactory
    {
        public static string GenerateConfirmationEmailHtml(string userName, string confirmationLink)
        {
            return $@"
            <p>Olá {userName},</p>
            <p>Clique no botão abaixo para confirmar seu e-mail:</p>
            <p><a style='padding: 10px 20px; background-color: #4CAF50; color: white; text-decoration: none;' href='{confirmationLink}'>Confirmar E-mail</a></p>";
        }
    }
}
