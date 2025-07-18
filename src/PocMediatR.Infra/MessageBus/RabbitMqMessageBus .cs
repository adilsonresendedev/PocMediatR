using Microsoft.Extensions.Options;
using PocMediatR.Infra.Settings;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PocMediatR.Infra.MessageBus
{
    public class RabbitMqMessageBus : IMessageBus
    {
        private readonly ConnectionFactory _factory;
        private readonly RabbitMqSettings _rabbitMqSettings;
        public RabbitMqMessageBus(IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            _rabbitMqSettings = rabbitMqSettings.Value;

            _factory = new ConnectionFactory
            {
                HostName = _rabbitMqSettings.HostName,
                UserName = _rabbitMqSettings.UserName,
                Password = _rabbitMqSettings.Password
            };
        }

        public async Task PublishAsync(string queue, object message)
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

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
