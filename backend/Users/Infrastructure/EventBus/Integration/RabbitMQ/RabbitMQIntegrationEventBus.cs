using RabbitMQ.Client;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediaLakeUsers.Infrastructure.EventBus.Integration.RabbitMQ
{
    public class RabbitMQIntegrationEventBus : IIntegrationEventBus
    {
        private readonly RabbitMQConnectionFactory _connectionFactory;

        public RabbitMQIntegrationEventBus(RabbitMQConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Publish<T>(T @event) where T : IntegrationEvent
        {
            //var eventName = @event.GetType().Name;

            //using (var channel = _connectionFactory.CreateChannel())
            //{
            //    channel.ExchangeDeclare(exchange: _connectionFactory.GetBrokerName(), type: "direct");

            //    var properties = channel.CreateBasicProperties();
            //    properties.Persistent = true;

            //    var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
            //    {
            //        WriteIndented = true
            //    });

            //    Console.WriteLine($"Publish event {eventName}");

            //    channel.BasicPublish(
            //            exchange: _connectionFactory.GetBrokerName(),
            //            routingKey: eventName,
            //            mandatory: true,
            //            basicProperties: properties,
            //            body: body);
            //}
        }
    }
}
