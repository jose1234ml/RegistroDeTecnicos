# Imagen base de ASP.NET 9 para producci�n
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Imagen SDK de .NET 9 para compilar y publicar
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia el archivo de proyecto y restaura dependencias
COPY ["RegistroDeTecnicos/RegistroDeTecnicos.csproj", "RegistroDeTecnicos/"]
WORKDIR /src/RegistroDeTecnicos
RUN dotnet restore "RegistroDeTecnicos.csproj"

# Copia el resto de los archivos y publica
COPY RegistroDeTecnicos/. .
RUN dotnet publish "RegistroDeTecnicos.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen final para ejecuci�n
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RegistroDeTecnicos.dll"]
