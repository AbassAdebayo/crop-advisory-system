# ============================================================
# Crop Advisory System (CAS) - Dockerfile
# Multi-stage build for ASP.NET Core MVC (.NET 10)
# ============================================================

# ------------------------------------------------------------
# Stage 1: Build
# Restores NuGet packages and publishes the application
# ------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files first (better layer caching)
# Adjust paths if your .csproj lives elsewhere
COPY ["CAS/CAS.csproj", "CAS/"]
COPY ["CAS.slnx", "./"]

# Restore dependencies
RUN dotnet restore "CAS/CAS.csproj"

# Copy the rest of the source code
COPY . .

# Publish in Release mode (self-contained not required; framework-dependent)
WORKDIR /src/CAS
RUN dotnet publish "CAS.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ------------------------------------------------------------
# Stage 2: Runtime
# Smaller image that only runs the published app
# ------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Install curl for health checks (optional but useful on Render)
USER root
RUN apt-get update && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

# Copy published output from build stage
COPY --from=build /app/publish .

# ASP.NET Core listens on port 8080 by default in containers
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose the port the app listens on
EXPOSE 8080

# Optional: non-root user for better security
# RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
# USER appuser

# Start the application
ENTRYPOINT ["dotnet", "CAS.dll"]
