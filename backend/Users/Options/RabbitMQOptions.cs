namespace MediaLakeUsers.Options
{
    public class RabbitMQOptions
    {
        public const string Location = "RabbitMQOptions";

        public string HostName { get; set; }
        public string Port { get; set; }
        public string QueueName { get; set; }
        public string BrokerName { get; set; }
    }
}
