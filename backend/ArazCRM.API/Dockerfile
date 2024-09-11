# 1. Aşama: Yapılandırma (Build) Aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ArazCRM.API.csproj", "."]
RUN dotnet restore "ArazCRM.API.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "ArazCRM.API.csproj" -c Release -o /app/build

# 2. Aşama: Yayınlama (Publish) Aşaması
FROM build AS publish
RUN dotnet publish "ArazCRM.API.csproj" -c Release -o /app/publish

# 3. Aşama: Çalıştırma (Run) Aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArazCRM.API.dll"]
