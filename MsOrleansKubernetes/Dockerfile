FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MsOrleansKubernetes/MsOrleansKubernetes.csproj", "MsOrleansKubernetes/"]

RUN dotnet restore "MsOrleansKubernetes/MsOrleansKubernetes.csproj"
COPY . .
WORKDIR "/src/MsOrleansKubernetes"
RUN dotnet build "MsOrleansKubernetes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MsOrleansKubernetes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MsOrleansKubernetes.dll"]