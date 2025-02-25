# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MamunTutorial.csproj", "./"]
RUN dotnet restore "MamunTutorial.csproj"
COPY . .
RUN dotnet publish "MamunTutorial.csproj" -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
# Force the application to use HTTP (port 80) and run in Production mode to disable HTTPS defaults
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "MamunTutorial.dll"]
