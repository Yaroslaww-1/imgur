using Amazon.CDK;

namespace MediaLakeCore.AWSCDK
{
    public class Program
    {
        static void Main(string[] args)
        {
            var app = new App();
            var configuration = new MediaLakeCoreAWSConfiguration();
            var stack = new MediaLakeCoreAWSStack(app, "MediaLakeCoreAWSStack", null, configuration);
            app.Synth();
        }
    }
}
