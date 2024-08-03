#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["FIAP.TECH.AUTH/FIAP.TECH.AUTH.csproj", "FIAP.TECH.AUTH/"]
COPY ["FIAP.TECH.CORE.APPLICATION/FIAP.TECH.CORE.APPLICATION.csproj", "FIAP.TECH.CORE.APPLICATION/"]
COPY ["FIAP.TECH.CORE.DOMAIN/FIAP.TECH.CORE.DOMAIN.csproj", "FIAP.TECH.CORE.DOMAIN/"]
COPY ["FIAP.TECH.INFRASTRUCTURE/FIAP.TECH.INFRASTRUCTURE.csproj", "FIAP.TECH.INFRASTRUCTURE/"]
RUN dotnet restore "./FIAP.TECH.AUTH/FIAP.TECH.AUTH.csproj"
COPY . .
WORKDIR "/src/FIAP.TECH.AUTH/FIAP.TECH.AUTH"
RUN dotnet build "./FIAP.TECH.AUTH.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FIAP.TECH.AUTH.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FIAP.TECH.AUTH.dll"]