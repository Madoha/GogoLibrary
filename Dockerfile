FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GogoLibrary.Api/GogoLibrary.Api.csproj", "GogoLibrary.Api/"]
COPY ["GogoLibrary.DAL/GogoLibrary.DAL.csproj", "GogoLibrary.DAL/"]
COPY ["GogoLibrary.Domain/GogoLibrary.Domain.csproj", "GogoLibrary.Domain/"]
COPY ["GogoLibrary.AI/GogoLibrary.AI.csproj", "GogoLibrary.AI/"]
COPY ["GogoLibrary.Application/GogoLibrary.Application.csproj", "GogoLibrary.Application/"]
RUN dotnet restore "GogoLibrary.Api/GogoLibrary.Api.csproj"
COPY . .
WORKDIR "/src/GogoLibrary.Api"
RUN dotnet build "GogoLibrary.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GogoLibrary.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GogoLibrary.Api.dll"]
