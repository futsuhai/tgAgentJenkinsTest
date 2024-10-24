FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TgInstanceAgent.Start/TgInstanceAgent.Start.csproj", "TgInstanceAgent.Start/"]
COPY ["TgInstanceAgent.Application.Abstractions/TgInstanceAgent.Application.Abstractions.csproj", "TgInstanceAgent.Application.Abstractions/"]
COPY ["TgInstanceAgent.Domain.Abstractions/TgInstanceAgent.Domain.Abstractions.csproj", "TgInstanceAgent.Domain.Abstractions/"]
COPY ["TgInstanceAgent.Domain/TgInstanceAgent.Domain.csproj", "TgInstanceAgent.Domain/"]
COPY ["TgInstanceAgent.Application.Services/TgInstanceAgent.Application.Services.csproj", "TgInstanceAgent.Application.Services/"]
COPY ["TgInstanceAgent.Infrastructure.Storage/TgInstanceAgent.Infrastructure.Storage.csproj", "TgInstanceAgent.Infrastructure.Storage/"]
COPY ["TgInstanceAgent.Infrastructure.Web/TgInstanceAgent.Infrastructure.Web.csproj", "TgInstanceAgent.Infrastructure.Web/"]
COPY ["TgInstanceAgent.Infrastructure.Telegram/TgInstanceAgent.Infrastructure.Telegram.csproj", "TgInstanceAgent.Infrastructure.Telegram/"]
RUN dotnet restore "TgInstanceAgent.Start/TgInstanceAgent.Start.csproj"
COPY . .
WORKDIR "/src/TgInstanceAgent.Start"
RUN dotnet build "TgInstanceAgent.Start.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TgInstanceAgent.Start.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TgInstanceAgent.Start.dll"]
