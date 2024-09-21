#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Health.ConsoleCommand/Health.ConsoleCommand.csproj", "Health.ConsoleCommand/"]
RUN dotnet restore "./Health.ConsoleCommand/./Health.ConsoleCommand.csproj"
COPY . .
WORKDIR "/src/Health.ConsoleCommand"
RUN dotnet build "./Health.ConsoleCommand.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Health.ConsoleCommand.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Health.ConsoleCommand.dll"]