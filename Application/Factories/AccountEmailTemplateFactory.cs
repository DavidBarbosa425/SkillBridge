using Application.Interfaces.Factories;

namespace Application.Factories
{
    public class AccountEmailTemplateFactory : IAccountEmailTemplateFactory
    {
        public string GenerateConfirmationEmailHtml(string userName, string link)
        {
            return $@"
            <p>Olá {userName},</p>
            <p>Clique no botão abaixo para confirmar seu e-mail:</p>
            <p><a style='padding: 10px 20px; background-color: #4CAF50; color: white; text-decoration: none;' href='{link}'>Confirmar E-mail</a></p>";
        }

        public string GeneratePasswordResetEmailHtml(string userName, string link)
        {
            return $@"
            <p>Olá {userName},</p>
            <p>Clique no botão abaixo para redefinir sua senha:</p>
            <p><a style='padding: 10px 20px; background-color: #4CAF50; color: white; text-decoration: none;' href='{link}'>Redefinir Senha</a></p>";
        }
    }
}
