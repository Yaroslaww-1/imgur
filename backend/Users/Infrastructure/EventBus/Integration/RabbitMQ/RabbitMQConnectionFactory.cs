using System;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using MediaLakeUsers.Options;

namespace MediaLakeUsers.Infrastructure.EventBus.Integration.RabbitMQ
{
    public class RabbitMQConnectionFactory : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private RabbitMQOptions _options;

        public RabbitMQConnectionFactory(IConfiguration configuration)
        {
            _options = configuration.GetSection(RabbitMQOptions.Location).Get<RabbitMQOptions>();

            //_connectionFactory = new ConnectionFactory() { HostName = _options.HostName, Port = Int32.Parse(_options.Port) };

            //_connection = _connectionFactory.CreateConnection();

            // Only for consuming
            //_channel = _connection.CreateModel();

            //_channel.QueueDeclare(
            //    queue: rabbitMQOptions.QueueName,
            //    durable: true,
            //    exclusive: false,
            //    autoDelete: false,
            //    arguments: null);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }

        public IModel CreateChannel()
        {
            return _connection.CreateModel();
        }

        public string GetBrokerName() => _options.BrokerName;
    }
}
