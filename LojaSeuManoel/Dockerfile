# Use a imagem do SDK do .NET 8.0 para construir a aplicação.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie o arquivo de projeto e restaure as dependências.
COPY ["LojaSeuManoel.csproj", "./"]
RUN dotnet restore "LojaSeuManoel.csproj"

# Copie o restante dos arquivos do projeto.
COPY . .

# Publique a aplicação.
RUN dotnet publish "LojaSeuManoel.csproj" -c Release -o /app/publish

# Use a imagem do ASP.NET Core runtime para executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/publish .

# Expõe a porta que a API usará.
EXPOSE 8080

# Comando para executar a aplicação.
ENTRYPOINT ["dotnet", "LojaSeuManoel.dll"]