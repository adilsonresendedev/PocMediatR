using Microsoft.Extensions.Options;
using PocMediatR.Infra.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace PocMediatR.Infra.MessageBus
{
    public class RabbitMqConsumerMessageBus : IConsumerMessageBus
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqConsumerMessageBus(IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            var settings = rabbitMqSettings.Value;
            _factory = new ConnectionFactory
            {
                HostName = settings.HostName,
                UserName = settings.UserName,
                Password = settings.Password
            };
        }

        public async Task ConsumeAsync<T>(string queue, Func<T, Task> onMessage, CancellationToken cancellationToken)
        {
            var connection = await _factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);
                    var message = JsonSerializer.Deserialize<T>(json);

                    if (message != null)
                        await onMessage(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            };

            await channel.BasicConsumeAsync(queue, autoAck: true, consumer);

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000, cancellationToken);
            }

            await channel.CloseAsync();
            await connection.CloseAsync();
        }
    }
}
