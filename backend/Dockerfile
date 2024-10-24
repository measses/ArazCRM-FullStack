
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app


COPY *.sln ./
COPY ArazCRM.API/*.csproj ./ArazCRM.API/
RUN dotnet restore

COPY . ./
WORKDIR /app/ArazCRM.API
RUN dotnet publish -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ArazCRM.API.dll"]
