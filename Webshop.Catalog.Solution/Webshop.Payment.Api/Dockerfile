FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Webshop.Payment.Api/Webshop.Payment.Api.csproj", "Webshop.Payment.Api/"]
RUN dotnet restore "Webshop.Payment.Api/Webshop.Payment.Api.csproj"
COPY . .
WORKDIR "/src/Webshop.Payment.Api"
RUN dotnet build "Webshop.Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Webshop.Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Webshop.Payment.Api.dll"]