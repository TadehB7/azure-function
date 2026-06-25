FROM public.ecr.aws/lambda/dotnet:10 AS base

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/WeatherEmailFunction/WeatherEmailFunction.csproj", "WeatherEmailFunction/"]
RUN dotnet restore "WeatherEmailFunction/WeatherEmailFunction.csproj"

COPY src/WeatherEmailFunction/ WeatherEmailFunction/
WORKDIR /src/WeatherEmailFunction

RUN dotnet publish "WeatherEmailFunction.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /var/task

COPY --from=build /app/publish .

CMD ["WeatherEmailFunction::WeatherEmailFunction.Function::FunctionHandler"]