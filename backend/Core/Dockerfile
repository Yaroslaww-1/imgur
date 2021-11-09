FROM mcr.microsoft.com/dotnet/sdk:5.0 AS builder
WORKDIR /app

COPY MediaLakeCore.csproj ./
RUN dotnet restore MediaLakeCore.csproj

COPY . ./source/
WORKDIR /app/source
RUN dotnet publish -c release -o /release

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /release
COPY --from=builder /release ./
ENTRYPOINT ["dotnet", "MediaLakeCore.dll"]