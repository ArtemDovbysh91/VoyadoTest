#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Install Node.js
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_16.x | bash - && \
apt-get install -y build-essential nodejs
# End Install
WORKDIR /src
COPY ["VoyadoTest/VoyadoTest.csproj", "VoyadoTest/"]
RUN dotnet restore "VoyadoTest/VoyadoTest.csproj"
COPY . .
WORKDIR "/src/VoyadoTest"
RUN dotnet build "VoyadoTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VoyadoTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VoyadoTest.dll"]