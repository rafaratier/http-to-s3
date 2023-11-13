# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0-jammy AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./Proxy.server.csproj" --disable-parallel
RUN dotnet publish "./Proxy.server.csproj" -c -release -o /app --no-restore

# Serve stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0-jammy 
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "Proxy.server.dll"]