#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV TZ="Asia/Shanghai"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Docker/ApiDockerDemo/ApiDockerDemo.csproj", "Docker/ApiDockerDemo/"]
RUN dotnet restore "Docker/ApiDockerDemo/ApiDockerDemo.csproj"
COPY . .
WORKDIR "/src/Docker/ApiDockerDemo"
RUN dotnet build "ApiDockerDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiDockerDemo.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiDockerDemo.dll"]