FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine-amd64 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine-amd64 AS build
WORKDIR /src
COPY ["Squip.Rest.csproj", "./"]
RUN dotnet restore
COPY . .
WORKDIR /src/.
RUN dotnet build "Squip.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Squip.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Squip.Rest.dll"]
