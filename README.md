# Media Lake
Media Lake is a media content aggregator

# Useful Commands

## Migrations
1. Change directory to *backend/Core*
2. Share your database connection string to env variables:\
`$env:DatabaseOptions__ConnectionString='Host=localhost;Port=7433;Database=postgres;Username=postgres;Password=YOUR_PASSWORD'`
3. Create migration:\
`dotnet ef migrations add MIGRATION_NAME --project MediaLakeCore.Infrastructure --startup-project MediaLakeCore.API --output-dir EntityFramework/Migrations`
4. Apply migration:\
`dotnet ef database update --project MediaLakeCore.Infrastructure --startup-project MediaLakeCore.API`