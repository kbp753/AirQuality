# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AirQuality.csproj", "./"]
RUN dotnet restore "./AirQuality.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "AirQuality.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AirQuality.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AirQuality.dll"]
