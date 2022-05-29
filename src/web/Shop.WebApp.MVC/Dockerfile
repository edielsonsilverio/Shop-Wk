FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./web/Shop.WebApp.MVC/Shop.WebApp.MVC.csproj", "web/Shop.WebApp.MVC/"]
#COPY ["./services/Shop.Business/Shop.Business.csproj", "services/Shop.Business/"]
#COPY ["./services/Shop.DataAccess/Shop.DataAccess.csproj", "services/Shop.DataAccess/"]
COPY ["./building blocks/Shop.Core/Shop.Core.csproj", "building blocks/Shop.Core/"]
COPY ["./building blocks/Shop.Core.WebApi/Shop.Core.WebApi.csproj", "building blocks/Shop.Core.WebApi/"]
COPY ["./building blocks/Shop.MessageBus/Shop.MessageBus.csproj", "building blocks/Shop.MessageBus/"]

RUN dotnet restore "web/Shop.WebApp.MVC/Shop.WebApp.MVC.csproj"
COPY . .
WORKDIR "web/Shop.WebApp.MVC"
RUN dotnet build "Shop.WebApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.WebApp.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.WebApp.MVC.dll"]