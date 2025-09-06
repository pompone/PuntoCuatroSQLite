# --- build ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos solución y csproj para aprovechar cache en restore
COPY PuntoCuatroSQLite.sln ./
COPY PuntoCuatroSQLite/PuntoCuatroSQLite.csproj PuntoCuatroSQLite/
RUN dotnet restore ./PuntoCuatroSQLite.sln

# Copiamos el resto del código y publicamos
COPY PuntoCuatroSQLite/ PuntoCuatroSQLite/
WORKDIR /src/PuntoCuatroSQLite
RUN dotnet publish PuntoCuatroSQLite.csproj -c Release -o /app/out

# --- runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Bindear al puerto que Render inyecta
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1

COPY --from=build /app/out ./

# Arranque
CMD ["dotnet", "PuntoCuatroSQLite.dll"]

