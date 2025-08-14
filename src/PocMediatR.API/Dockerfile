# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only project files first (better caching)
COPY src/PocMediatR.API/PocMediatR.API.csproj src/PocMediatR.API/
COPY src/PocMediatR.Application/PocMediatR.Application.csproj src/PocMediatR.Application/
COPY src/PocMediatR.Common/PocMediatR.Common.csproj src/PocMediatR.Common/
COPY src/PocMediatR.Domain/PocMediatR.Domain.csproj src/PocMediatR.Domain/
COPY src/PocMediatR.Infra/PocMediatR.Infra.csproj src/PocMediatR.Infra/

# Restore dependencies
RUN dotnet restore src/PocMediatR.API/PocMediatR.API.csproj

# Copy everything else
COPY . .

# Build and publish
WORKDIR /src/src/PocMediatR.API
RUN dotnet publish PocMediatR.API.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PocMediatR.API.dll"]
