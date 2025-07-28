namespace Domain.Interfaces
{
    public interface IMessageBrokerService
    {
        Task PublishAsync<T>(T message, string queueName) where T : class;
        Task PublishAsync<T>(T message) where T : class;
    }
}
