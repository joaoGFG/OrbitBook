FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["OrbitBook.API/OrbitBook.API.csproj", "OrbitBook.API/"]
COPY ["OrbitBook.Application/OrbitBook.Application.csproj", "OrbitBook.Application/"]
COPY ["OrbitBook.Domain/OrbitBook.Domain.csproj", "OrbitBook.Domain/"]
COPY ["OrbitBook.Infrastructure/OrbitBook.Infrastructure.csproj", "OrbitBook.Infrastructure/"]

RUN dotnet restore "OrbitBook.API/OrbitBook.API.csproj"

COPY . .

WORKDIR "/src/OrbitBook.API"
RUN dotnet build "OrbitBook.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OrbitBook.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrbitBook.API.dll"]
