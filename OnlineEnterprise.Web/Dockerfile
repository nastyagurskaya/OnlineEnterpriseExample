FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 50604
EXPOSE 44321

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ../OnlineEnterprise.Web/OnlineEnterprise.Web.csproj ../OnlineEnterprise.Web/
RUN dotnet restore ../OnlineEnterprise.Web/OnlineEnterprise.Web.csproj
COPY . .
WORKDIR /src/../OnlineEnterprise.Web
RUN dotnet build OnlineEnterprise.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish OnlineEnterprise.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OnlineEnterprise.Web.dll"]
