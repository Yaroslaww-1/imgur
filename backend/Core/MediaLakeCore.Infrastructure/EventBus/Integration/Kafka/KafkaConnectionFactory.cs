using Confluent.Kafka;
using MediaLakeCore.BuildingBlocks.Infrastructure.Options;
using Microsoft.Extensions.Configuration;

namespace MediaLakeCore.Infrastructure.EventBus.Integration.Kafka
{
    public class KafkaConnectionFactory
    {
        private KafkaOptions _options;
        private ConsumerConfig _consumerConfig;

        public KafkaConnectionFactory(IConfiguration configuration)
        {
            _options = configuration.GetSection(KafkaOptions.Location).Get<KafkaOptions>();

            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _options.BootstrapServers,
                GroupId = _options.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                MaxPollIntervalMs = 10000,
                SessionTimeoutMs = 10000
            };
        }

        public ConsumerConfig GetConsumerConfig() => _consumerConfig;
    }
}
