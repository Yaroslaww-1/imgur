namespace MediaLakeGatewayApi.Options
{
    public class MonitoringOptions
    {
        public const string Location = "MonitoringOptions";

        public bool UseMonitoring { get; set; }

        public bool UseHttpMetrics { get; set; }
        public bool UseHealthChecks { get; set; }
    }
}
