# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/PocMediatR.EntitySyncWorker/PocMediatR.EntitySyncWorker.csproj", "src/PocMediatR.EntitySyncWorker/"]
COPY ["src/PocMediatR.Infra/PocMediatR.Infra.csproj", "src/PocMediatR.Infra/"]
COPY ["src/PocMediatR.Domain/PocMediatR.Domain.csproj", "src/PocMediatR.Domain/"]
COPY ["src/PocMediatR.Application/PocMediatR.Application.csproj", "src/PocMediatR.Application/"]

# Restore dependencies
RUN dotnet restore "src/PocMediatR.EntitySyncWorker/PocMediatR.EntitySyncWorker.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/src/PocMediatR.EntitySyncWorker"
RUN dotnet publish "PocMediatR.EntitySyncWorker.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Copy SQL scripts (adjust path if needed)
COPY src/PocMediatR.EntitySyncWorker/Persistence/Scripts /app/Persistence/Scripts

ENTRYPOINT ["dotnet", "PocMediatR.EntitySyncWorker.dll"]
