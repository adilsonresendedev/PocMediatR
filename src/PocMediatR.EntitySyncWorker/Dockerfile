# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only project files first (better caching)
COPY src/PocMediatR.EntitySyncWorker/PocMediatR.EntitySyncWorker.csproj src/PocMediatR.EntitySyncWorker/
COPY src/PocMediatR.Domain/PocMediatR.Domain.csproj src/PocMediatR.Domain/
COPY src/PocMediatR.Common/PocMediatR.Common.csproj src/PocMediatR.Common/
COPY src/PocMediatR.Infra/PocMediatR.Infra.csproj src/PocMediatR.Infra/

# Restore dependencies
RUN dotnet restore src/PocMediatR.EntitySyncWorker/PocMediatR.EntitySyncWorker.csproj

# Copy everything else
COPY . .

# Build and publish
WORKDIR /src/src/PocMediatR.EntitySyncWorker
RUN dotnet publish PocMediatR.EntitySyncWorker.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime stage
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PocMediatR.EntitySyncWorker.dll"]
