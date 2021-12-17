FROM mcr.microsoft.com/dotnet/sdk:5.0 AS builder
WORKDIR /app

COPY MediaLakeCore.AWSCDK.csproj ./
RUN dotnet restore MediaLakeCore.AWSCDK.csproj

COPY . ./source/
WORKDIR /app/source
RUN dotnet publish -c release -o /release

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /release
COPY --from=builder /release ./

RUN apt-get update && apt-get install -y curl
RUN curl -sL https://deb.nodesource.com/setup_16.x | bash - \ 
	&& apt update \
	&& apt install -y nodejs

RUN npm i -g aws-cdk
# && cdk deploy --app "dotnet MediaLakeCore.AWSCDK.dll"
ENTRYPOINT [ "cdk deploy" ]