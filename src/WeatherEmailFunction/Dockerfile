# Dockerfile

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

# Set environment WeatherEmailFunction
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production


FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["./src/WeatherEmailFunction/WeatherEmailFunction.csproj", "WeatherEmailFunction/"]

# Restore dependencies
RUN dotnet restore "WeatherEmailFunction/WeatherEmailFunction.csproj"

# Copy all source code
COPY ./src .
WORKDIR "/src/WeatherEmailFunction"

# Build the application
RUN dotnet build "./WeatherEmailFunction.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WeatherEmailFunction.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

# Create a non-root user (Debian/Ubuntu style)
RUN useradd -m appuser && chown -R appuser /app

USER appuser

ENTRYPOINT ["dotnet", "WeatherEmailFunction.dll"]
