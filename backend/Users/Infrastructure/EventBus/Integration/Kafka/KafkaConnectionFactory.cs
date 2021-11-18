using System;
using Microsoft.Extensions.Configuration;
using MediaLakeUsers.Options;
using Confluent.Kafka;

namespace MediaLakeUsers.Infrastructure.EventBus.Integration.Kafka
{
    public class KafkaConnectionFactory
    {
        private KafkaOptions _options;
        private ProducerConfig _producerConfig;

        public KafkaConnectionFactory(IConfiguration configuration)
        {
            _options = configuration.GetSection(KafkaOptions.Location).Get<KafkaOptions>();

            _producerConfig = new ProducerConfig { BootstrapServers = _options.BootstrapServers };
        }

        public ProducerConfig GetProducerConfig() => _producerConfig;
    }
}
