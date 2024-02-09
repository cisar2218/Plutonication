# Use Microsoft's official .NET 7 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy everything and restore as distinct layers
COPY . ./
RUN dotnet restore

# Build and publish the application in a self-contained mode to reduce runtime dependencies
RUN dotnet publish -c Release -o out --no-restore

# Use Microsoft's official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/runtime:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# Command to run the application
ENTRYPOINT ["dotnet", "PlutonicationConsoleSample.dll"]