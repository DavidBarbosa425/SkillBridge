using Application.Interfaces.Emails;
using Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Consumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegistered>
    {
        private readonly IEmailAccountService _emailAccountService;
        private readonly ILogger<UserRegisteredConsumer> _logger;

        public UserRegisteredConsumer(
            IEmailAccountService emailAccountService,
            ILogger<UserRegisteredConsumer> logger)
        {
            _emailAccountService = emailAccountService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserRegistered> context)
        {
            try
            {
                var userRegistered = context.Message;

                await _emailAccountService.SendConfirmationEmailAsync(userRegistered);

                _logger.LogInformation($"Email processado com sucesso para: {userRegistered.Email}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar email");
                throw;
            }
        }
    }
}
