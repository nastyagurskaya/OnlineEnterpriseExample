FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY OnlineEnterpriseOrders.Web/OnlineEnterpriseOrders.Web.csproj OnlineEnterpriseOrders.Web/
COPY OnlineEnterprice.Data/OnlineEnterprise.Data.csproj OnlineEnterprice.Data/
COPY OnlineEnterprice.Domain/OnlineEnterprise.Domain.csproj OnlineEnterprice.Domain/
RUN dotnet restore OnlineEnterpriseOrders.Web/OnlineEnterpriseOrders.Web.csproj
COPY . .
WORKDIR /src/OnlineEnterpriseOrders.Web
RUN dotnet build OnlineEnterpriseOrders.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish OnlineEnterpriseOrders.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OnlineEnterpriseOrders.Web.dll"]
