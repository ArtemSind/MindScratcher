FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MindScratcher/MindScratcher.csproj", "MindScratcher/"]
RUN dotnet restore "MindScratcher/MindScratcher.csproj"
COPY . .
WORKDIR "/src/MindScratcher"
RUN dotnet build "MindScratcher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MindScratcher.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MindScratcher.dll"]
