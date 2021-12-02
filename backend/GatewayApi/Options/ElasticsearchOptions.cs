namespace MediaLakeGatewayApi.Options
{
    public class ElasticsearchOptions
    {
        public const string Location = "ElasticsearchOptions";

        public string ConnectionString { get; set; }
        public string IndexFormat { get; set; }
    }
}
