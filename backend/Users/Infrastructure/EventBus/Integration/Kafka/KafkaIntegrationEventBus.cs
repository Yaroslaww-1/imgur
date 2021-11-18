using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLakeUsers.Infrastructure.EventBus.Integration.Kafka
{
    public class KafkaIntegrationEventBus : IIntegrationEventBus
    {
        private readonly KafkaConnectionFactory _connectionFactory;

        public KafkaIntegrationEventBus(KafkaConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Publish<T>(T @event) where T : IntegrationEvent
        {
            using var producer = new ProducerBuilder<Null, string>(_connectionFactory.GetProducerConfig()).Build();
            try
            {
                var eventType = @event.GetType().Name;
                var eventHeaders = new Headers
                {
                    new Header("eventType", Encoding.UTF8.GetBytes(eventType))
                };

                var topicName = @event.AggregateEntityName;

                Console.WriteLine($"Topic name {topicName}");

                var deliveryResult = await producer.ProduceAsync(topicName, new Message<Null, string> { Headers = eventHeaders, Value = JsonConvert.SerializeObject(@event) });
                Console.WriteLine($"KafkaIntegrationEventBus: Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"KafkaIntegrationEventBus: Delivery failed: {e.Error.Reason}");
            }
        }
    }
}
