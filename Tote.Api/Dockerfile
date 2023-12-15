FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Tote.Api/Tote.Api.csproj", "Tote.Api/"]
RUN dotnet restore "Tote.Api/Tote.Api.csproj"
COPY . .
WORKDIR "/src/Tote.Api"
RUN dotnet build "Tote.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tote.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tote.Api.dll"]
