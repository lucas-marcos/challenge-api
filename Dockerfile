FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto
COPY ["ServiceControl.API/ServiceControl.API.csproj", "ServiceControl.API/"]
COPY ["ServiceControl.Application/ServiceControl.Application.csproj", "ServiceControl.Application/"]
COPY ["ServiceControl.Domain/ServiceControl.Domain.csproj", "ServiceControl.Domain/"]
COPY ["ServiceControl.Infrastructure/ServiceControl.Infrastructure.csproj", "ServiceControl.Infrastructure/"]

# Restaura as dependências
RUN dotnet restore "ServiceControl.API/ServiceControl.API.csproj"

# Copia todo o código fonte
COPY . .

# Build da aplicação
WORKDIR "/src/ServiceControl.API"
RUN dotnet build "ServiceControl.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceControl.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceControl.API.dll"]
