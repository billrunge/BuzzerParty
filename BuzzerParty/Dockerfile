#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV SQL_SERVER="172.17.0.2"
ENV SQL_DATABASE_NAME="Jeopardivity"
ENV SQL_USER_NAME="sa"
ENV SQL_PASSWORD="Test1234!"
ENV SQL_TIMEOUT="30"
ENV SQL_PORT="1433"
ENV APP_NAME="Freddie Party"
ENV JWT_KEY="0mgooh3w20lpO0PxI54RbpVoua0qJy0IoROGarNJm3aJU9qJJIONVF1OvPVL9DsyzG7ftBbjThFEYV5hkcp1u4l7uduyt3ntHeOhdrozgnFF8ygjG4S5IKGmvvVQ6NVY"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BuzzerParty/BuzzerParty.csproj", "BuzzerParty/"]
RUN dotnet restore "BuzzerParty/BuzzerParty.csproj"
COPY . .
WORKDIR "/src/BuzzerParty"
RUN dotnet build "BuzzerParty.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BuzzerParty.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BuzzerParty.dll"]