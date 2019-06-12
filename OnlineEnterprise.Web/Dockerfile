FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY OnlineEnterprise.Web/OnlineEnterprise.Web.csproj OnlineEnterprise.Web/
COPY OnlineEnterprice.Data/OnlineEnterprise.Data.csproj OnlineEnterprice.Data/
COPY OnlineEnterprice.Domain/OnlineEnterprise.Domain.csproj OnlineEnterprice.Domain/
RUN dotnet restore OnlineEnterprise.Web/OnlineEnterprise.Web.csproj
COPY . .
WORKDIR /src/OnlineEnterprise.Web
RUN dotnet build OnlineEnterprise.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish OnlineEnterprise.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OnlineEnterprise.Web.dll"]
