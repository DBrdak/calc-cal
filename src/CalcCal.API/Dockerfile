#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CalcCal.API/CalcCal.API.csproj", "src/CalcCal.API/"]
COPY ["src/CalcCal.Application/CalcCal.Application.csproj", "src/CalcCal.Application/"]
COPY ["src/CalcCal.Domain/CalcCal.Domain.csproj", "src/CalcCal.Domain/"]
COPY ["src/CalcCal.Infrastructure/CalcCal.Infrastructure.csproj", "src/CalcCal.Infrastructure/"]
RUN dotnet restore "./src/CalcCal.API/./CalcCal.API.csproj"
COPY . .
WORKDIR "/src/src/CalcCal.API"
RUN dotnet build "./CalcCal.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CalcCal.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalcCal.API.dll"]