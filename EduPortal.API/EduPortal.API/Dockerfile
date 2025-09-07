#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EduPortal.API/EduPortal.API.csproj", "EduPortal.API/"]
COPY ["EduPortal.Bl/EduPortal.Bl.csproj", "EduPortal.Bl/"]
COPY ["EduPortal.Dto/EduPortal.Dto.csproj", "EduPortal.Dto/"]
COPY ["EduPortal.Dal/EduPortal.Dal.csproj", "EduPortal.Dal/"]
RUN dotnet restore "./EduPortal.API/EduPortal.API.csproj"
COPY . .
WORKDIR "/src/EduPortal.API"
RUN dotnet build "./EduPortal.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EduPortal.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EduPortal.API.dll"]