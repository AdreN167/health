#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Health.Api/Health.Api.csproj", "Health.Api/"]
COPY ["Health.Core/Health.Core.csproj", "Health.Core/"]
COPY ["Health.DAL/Health.DAL.csproj", "Health.DAL/"]
COPY ["Health.Domain/Health.Domain.csproj", "Health.Domain/"]
RUN dotnet restore "./Health.Api/./Health.Api.csproj"
COPY . .
WORKDIR "/src/Health.Api"
RUN dotnet build "./Health.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Health.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir -p /app/wwwroot/uploads
RUN chown -R www-data:www-data /app/wwwroot/uploads
RUN chmod -R 755 /app/wwwroot/uploads
# RUN cd ..
# RUN chmod -R 777 /
# RUN mkdir -p /wwwroot/uploads/products && chmod -R 777 /wwwroot/uploads/products
ENTRYPOINT ["dotnet", "Health.Api.dll"]