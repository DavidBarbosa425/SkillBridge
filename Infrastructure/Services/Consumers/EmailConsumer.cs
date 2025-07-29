using MassTransit;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Consumers
{
    public class EmailConsumer : IConsumer<EmailMessage>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailConsumer> _logger;

        public EmailConsumer(IEmailService emailService, ILogger<EmailConsumer> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<EmailMessage> context)
        {
            try
            {
                var message = context.Message;

                var sendEmail = new SendEmail
                {
                    Name = message.Name,
                    Email = message.To,
                    Subject = message.Subject,
                    Body = message.Body
                };

                await _emailService.SendEmailAsync(sendEmail);

                _logger.LogInformation($"Email processado com sucesso para: {message.To}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar email");
                throw new Exception(ex.Message);
            }
        }
    }
}