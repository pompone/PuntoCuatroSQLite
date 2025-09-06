# --- build ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copia solución y csproj para restaurar
COPY PuntoCuatroSQLite.sln ./
COPY PuntoCuatroSQLite/PuntoCuatroSQLite.csproj PuntoCuatroSQLite/
RUN dotnet restore

# copia el resto y publica
COPY PuntoCuatroSQLite/ PuntoCuatroSQLite/
WORKDIR /src/PuntoCuatroSQLite
RUN dotnet publish -c Release -o /app/out

# --- runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/out ./

# Render expone el puerto en la var $PORT; lo usamos al arrancar
CMD ["sh","-c","ASPNETCORE_URLS=http://0.0.0.0:$PORT dotnet PuntoCuatroSQLite.dll"]
