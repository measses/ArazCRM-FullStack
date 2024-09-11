# 1. Aşama: Yapılandırma (Build) Aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BestCloudCase-API.csproj", "."]
RUN dotnet restore "BestCloudCase-API.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "BestCloudCase-API.csproj" -c Release -o /app/build

# 2. Aşama: Yayınlama (Publish) Aşaması
FROM build AS publish
RUN dotnet publish "BestCloudCase-API.csproj" -c Release -o /app/publish

# 3. Aşama: Çalıştırma (Run) Aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BestCloudCase-API.dll"]
