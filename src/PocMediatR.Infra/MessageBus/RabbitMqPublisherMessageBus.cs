using Microsoft.Extensions.Options;
using PocMediatR.Infra.Settings;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PocMediatR.Infra.MessageBus
{
    public class RabbitMqPublisherMessageBus : IPublisherMessageBus
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqPublisherMessageBus(IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            var settings = rabbitMqSettings.Value;
            _factory = new ConnectionFactory
            {
                HostName = settings.HostName,
                UserName = settings.UserName,
                Password = settings.Password
            };
        }

        public async Task PublishAsync<T>(string queue, T message, CancellationToken cancellationToken)
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            var properties = new BasicProperties
            {
                Persistent = true
            };

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queue,
                mandatory: false,
                basicProperties: properties,
                body: body
            );
        }
    }
}
