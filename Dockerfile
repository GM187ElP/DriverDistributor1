# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution file
COPY *.sln ./

# Copy project folder (with .csproj inside)
COPY DriverDistributor/ ./DriverDistributor/

# Restore all projects in solution
RUN dotnet restore

# Copy all source files
COPY . .

# Publish
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DriverDistributor.dll"]
