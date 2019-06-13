FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY OnlineEnterpriseExmpl/OnlineEnterpriseExmpl.csproj OnlineEnterpriseExmpl/
COPY OnlineEnterprice.Data/OnlineEnterprise.Data.csproj OnlineEnterprice.Data/
COPY OnlineEnterprice.Domain/OnlineEnterprise.Domain.csproj OnlineEnterprice.Domain/
RUN dotnet restore OnlineEnterpriseExmpl/OnlineEnterpriseExmpl.csproj
COPY . .
WORKDIR /src/OnlineEnterpriseExmpl
RUN dotnet build OnlineEnterpriseExmpl.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish OnlineEnterpriseExmpl.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OnlineEnterpriseExmpl.dll"]
