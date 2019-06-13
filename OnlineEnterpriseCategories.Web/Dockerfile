FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY OnlineEnterpriseCategories.Web/OnlineEnterpriseCategories.Web.csproj OnlineEnterpriseCategories.Web/
COPY OnlineEnterprice.Data/OnlineEnterprise.Data.csproj OnlineEnterprice.Data/
COPY OnlineEnterprice.Domain/OnlineEnterprise.Domain.csproj OnlineEnterprice.Domain/
RUN dotnet restore OnlineEnterpriseCategories.Web/OnlineEnterpriseCategories.Web.csproj
COPY . .
WORKDIR /src/OnlineEnterpriseCategories.Web
RUN dotnet build OnlineEnterpriseCategories.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish OnlineEnterpriseCategories.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OnlineEnterpriseCategories.Web.dll"]
