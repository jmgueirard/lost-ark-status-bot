FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["LostArkStatus/LostArkStatus.csproj", "LostArkStatus/"]
RUN dotnet restore "LostArkStatus/LostArkStatus.csproj"
COPY . .
WORKDIR "/src/LostArkStatus"
RUN dotnet build "LostArkStatus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LostArkStatus.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LostArkStatus.dll"]
