// Infrastructure/Services/RabbitMqService.cs
using Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class RabbitMqService : IMessageBrokerService
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<RabbitMqService> _logger;

        public RabbitMqService(IPublishEndpoint publishEndpoint, ILogger<RabbitMqService> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task PublishAsync<T>(T message, string queueName) where T : class
        {
            try
            {
                await _publishEndpoint.Publish(message);
                _logger.LogInformation($"Mensagem publicada na fila: {queueName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao publicar mensagem na fila: {queueName}");
                throw;
            }
        }

        public async Task PublishAsync<T>(T message) where T : class
        {
            try
            {
                await _publishEndpoint.Publish(message);
                _logger.LogInformation($"Mensagem publicada: {typeof(T).Name}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao publicar mensagem: {typeof(T).Name}");
                throw;
            }
        }
    }
}