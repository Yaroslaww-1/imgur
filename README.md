# Media Lake
Media Lake is a media content aggregator

# Architecture

![image](https://user-images.githubusercontent.com/40521835/147410912-b3ca487a-1260-4327-96c5-0838ad49abcf.png)

# Useful Commands

## AWS Configuration

Application uses AWS CDK. To use it you need to complete following steps:
1. Configure your AWS Profile and install cdk itself. Please visit [this](https://docs.aws.amazon.com/cdk/v2/guide/work-with.html) link to get more details.
2. Set `AWSOptions__Environment` environment variable to `YOUR_NAME-local` and override it in docker-compose.yml's `media_lake_core_api` container.
3. Run `cd ./backend/Core/MediaLakeCore.AWSCDK && cdk deploy`

## Migrations
1. Change directory to *backend/Core*
2. Share your database connection string to env variables:\
`$env:DatabaseOptions__ConnectionString='Host=localhost;Port=7433;Database=postgres;Username=postgres;Password=YOUR_PASSWORD'`
3. Create migration:\
`dotnet ef migrations add MIGRATION_NAME --project MediaLakeCore.Infrastructure --startup-project MediaLakeCore.API --output-dir EntityFramework/Migrations`
4. Apply migration:\
`dotnet ef database update --project MediaLakeCore.Infrastructure --startup-project MediaLakeCore.API`
