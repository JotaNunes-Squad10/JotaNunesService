FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["JotaNunes.Api/JotaNunes.Api.csproj", "JotaNunes.Api/"]
RUN dotnet restore "JotaNunes.Api/JotaNunes.Api.csproj"
COPY . .
WORKDIR "/src/JotaNunes.Api"
RUN dotnet build "./JotaNunes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./JotaNunes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JotaNunes.Api.dll"]
