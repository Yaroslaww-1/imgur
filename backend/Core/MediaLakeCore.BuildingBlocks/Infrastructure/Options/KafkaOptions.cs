namespace MediaLakeCore.BuildingBlocks.Infrastructure.Options
{
    public class KafkaOptions
    {
        public const string Location = "KafkaOptions";

        public string BootstrapServers { get; set; }
        public string GroupId { get; set; }
    }
}
