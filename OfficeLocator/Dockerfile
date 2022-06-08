FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OfficeLocator/OfficeLocator.csproj", "OfficeLocator/"]
RUN dotnet restore "OfficeLocator/OfficeLocator.csproj"
COPY . .
WORKDIR "/src/OfficeLocator"
RUN dotnet build "OfficeLocator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OfficeLocator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OfficeLocator.dll"]
