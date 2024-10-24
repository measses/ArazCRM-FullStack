FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY ArazCRM.API.sln ./
COPY ArazCRM.API.DTO/*.csproj ./ArazCRM.API.DTO/
COPY ArazCRM.API/*.csproj ./ArazCRM.API/
COPY ArazCRM.API.Data/*.csproj ./ArazCRM.API.Data/
COPY ArazCRM.API.Models/*.csproj ./ArazCRM.API.Models/
COPY ArazCRM.API.Repositories/*.csproj ./ArazCRM.API.Repositories/
COPY ArazCRM.API.Services/*.csproj ./ArazCRM.API.Services/

RUN dotnet restore

COPY . ./
WORKDIR /app/ArazCRM.API
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ArazCRM.API.dll"]
