FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80/
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY "Directory.Build.props" .
COPY "Directory.Packages.props" .
COPY ["src/DictionaryApi/DictionaryApi.csproj", "src/DictionaryApi/"]
RUN dotnet restore "src/DictionaryApi/DictionaryApi.csproj"
COPY . .
WORKDIR "/src/src/DictionaryApi"
RUN dotnet build "DictionaryApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DictionaryApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DictionaryApi.dll"]
