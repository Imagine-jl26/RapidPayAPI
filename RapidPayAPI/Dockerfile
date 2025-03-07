# Use .NET 8 SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

# Copy solution file and restore dependencies
COPY ["RapidPayAPI.sln", "./"]
COPY ["RapidPayAPI/RapidPayAPI.csproj", "RapidPayAPI/"]
RUN dotnet restore "RapidPayAPI/RapidPayAPI.csproj"

# Copy all project files
COPY . . 
WORKDIR "/app/RapidPayAPI"

# Build the application
RUN dotnet build "RapidPayAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish	
RUN dotnet publish "RapidPayAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RapidPayAPI.dll"]
