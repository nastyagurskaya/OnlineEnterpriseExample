using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new[]
            {
                new ApiResource("products_api", "Products Api with Swagger"),
                new ApiResource("orders_api", "Orders Api with Swagger"),
                new ApiResource("categories_api", "Categories Api with Swagger")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "products_api_swagger",
                    ClientName = "Products Api Swagger",
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    // secret for authentication
                    RedirectUris =
                    {
                        "http://localhost:51490/swagger/oauth2-redirect.html",
                        "http://localhost:51490/o2c.html"
                    },
                    // scopes that client has access to
                    AllowedScopes = { "products_api",IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile }
                },
                new Client
                {
                    ClientId = "orders_api_swagger",
                    ClientName = "Orders Api Swagger",
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    // secret for authentication
                    RedirectUris =
                    {
                        "http://localhost:51491/swagger/oauth2-redirect.html",
                        "http://localhost:51491/o2c.html"
                    },
                    // scopes that client has access to
                    AllowedScopes = { "orders_api",IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile }
                },
                new Client
                {
                    ClientId = "categories_api_swagger",
                    ClientName = "Orders Api Swagger",
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    // secret for authentication
                    RedirectUris =
                    {
                        "http://localhost:51492/swagger/oauth2-redirect.html",
                        "http://localhost:51492/o2c.html"
                    },
                    // scopes that client has access to
                    AllowedScopes = { "categories_api",IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile }
                },
            };
        }
    }
}