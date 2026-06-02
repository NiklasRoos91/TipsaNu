FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiera projektfilerna för att återställa beroenden
COPY ["TipsaNu.Api/TipsaNu.Api.csproj", "TipsaNu.Api/"]
COPY ["TipsaNu.Infrastructure/TipsaNu.Infrastructure.csproj", "TipsaNu.Infrastructure/"]
RUN dotnet restore "TipsaNu.Api/TipsaNu.Api.csproj"

# Kopiera resten av koden och bygg appen
COPY . .
WORKDIR "/src/TipsaNu.Api"
RUN dotnet publish "TipsaNu.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Skapa slutgiltig image baserad på ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "TipsaNu.Api.dll"]