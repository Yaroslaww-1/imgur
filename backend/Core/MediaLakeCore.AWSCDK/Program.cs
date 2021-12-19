using Amazon.CDK;
using Microsoft.Extensions.Configuration;

namespace MediaLakeCore.AWSCDK
{
    public class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .AddEnvironmentVariables()
                .Build();

            var awsOptions = configuration.GetSection(AWSOptions.Location).Get<AWSOptions>();

            var app = new App();
            var stack = new MediaLakeCoreAWSStack(
                app,
                "MediaLakeCoreAWSStack",
                new StackProps()
                {
                    Env = new Environment()
                    {
                        Region = awsOptions.Region
                    }
                },
                new MediaLakeCoreAWSConfiguration(awsOptions)
            );
            app.Synth();
        }
    }
}
