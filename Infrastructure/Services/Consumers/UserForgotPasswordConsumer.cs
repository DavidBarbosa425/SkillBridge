using Application.Interfaces.Emails;
using Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Consumers
{
    public class UserForgotPasswordConsumer : IConsumer<UserForgotPassword>
    {
        private readonly IEmailAccountService _emailAccountService;
        private readonly ILogger<UserForgotPasswordConsumer> _logger;

        public UserForgotPasswordConsumer(
            IEmailAccountService emailAccountService,
            ILogger<UserForgotPasswordConsumer> logger)
        {
            _emailAccountService = emailAccountService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserForgotPassword> context)
        {
            try
            {
                var userForgotPassword = context.Message;

                await _emailAccountService.SendPasswordResetEmailAsync(userForgotPassword);

                _logger.LogInformation($"Email processado com sucesso para: {userForgotPassword.Email}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar email");
                throw;
            }
        }
    }
}
