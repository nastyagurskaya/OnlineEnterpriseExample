FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["OnlineEnterprise.IdentityServer/OnlineEnterprise.IdentityServer.csproj", "OnlineEnterprise.IdentityServer/"]
RUN dotnet restore "OnlineEnterprise.IdentityServer/OnlineEnterprise.IdentityServer.csproj"
COPY . .
WORKDIR "/src/OnlineEnterprise.IdentityServer"
RUN dotnet build "OnlineEnterprise.IdentityServer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OnlineEnterprise.IdentityServer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OnlineEnterprise.IdentityServer.dll"]