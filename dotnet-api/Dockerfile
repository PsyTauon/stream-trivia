#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["dotnet-api.csproj", ""]
RUN dotnet restore "./dotnet-api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "dotnet-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dotnet-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5000
ENTRYPOINT ["dotnet", "dotnet-api.dll"]