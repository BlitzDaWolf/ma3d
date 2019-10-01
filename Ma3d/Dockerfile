FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Ma3d/Ma3d.csproj", "Ma3d/"]
RUN dotnet restore "Ma3d/Ma3d.csproj"
COPY . .
WORKDIR "/src/Ma3d"
RUN dotnet build "Ma3d.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ma3d.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ma3d.dll"]