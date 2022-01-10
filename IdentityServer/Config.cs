// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource(name: "user", userClaims: new[] { JwtClaimTypes.Email })
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1"),
                new ApiScope("scope2"),
                new ApiScope("mybook")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               new Client
               {
                   ClientId = "postman",
                   ClientName = "client for postman",
                   AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                   ClientSecrets = { new Secret("secret".Sha256()) },
                   AllowedScopes = { "mybook", "user" },
                   AlwaysSendClientClaims = true,
                   AlwaysIncludeUserClaimsInIdToken = true,
                   AllowAccessTokensViaBrowser = true
               },
               new Client
               {
                   ClientId = "swagger",
                   ClientName = "client for swagger",
                   AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                   ClientSecrets = { new Secret("secret".Sha256()) },
                   AllowedScopes = { "mybook", "user", "openid" },
                   AlwaysSendClientClaims = true,
                   AlwaysIncludeUserClaimsInIdToken = true,
                   AllowAccessTokensViaBrowser = true,
                   RedirectUris = { "https://localhost:44389/swagger/oauth2-redirect.html" },
                   AllowedCorsOrigins = { "https://localhost:44389" }
               }
            };
    }
}