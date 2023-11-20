﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace AnimeShopping.IdentityServe.Configuration
{
    public static class IdentityConfiguration
    {
        public const string Admin = "Admin";
        public const string Client = "Client";
        private static string secret = "302b0160-3123-41f3-9b1f-a6f0e15dcb8e";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("anime_shopping", "AnimeShopping Server"),
                new ApiScope(name: "read", "Read data"),
                new ApiScope(name: "write", "Write data"),
                new ApiScope(name: "delete", "Delete data")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = {new Secret(secret.Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"read", "write", "profile"}
                },
                new Client
                {
                    ClientId = "anime_shopping",
                    ClientSecrets = {new Secret(secret.Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = {"http://localhost:34312/signin-oidc"},
                    PostLogoutRedirectUris = {"http://localhost:34312/signout-callback-oidc"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        "anime_shopping"
                    }
                },
            };
    }
}
