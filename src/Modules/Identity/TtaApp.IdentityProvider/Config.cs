﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityModel;
using TtaApp.IdentityProvider.Resources;

namespace TtaApp.IdentityProvider
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("ttaapp"),
            };

        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),

                // let's include the role claim in the profile
                new ProfileWithRoleIdentityResource(),
                new IdentityResources.Email()
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                // the api requires the role claim
                new ApiResource(
                    "ttaapp",
                    "TtaApp",
                    new[]
                    {
                        JwtClaimTypes.Role
                    }
                )
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "client-app",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins =
                    {
                        "https://localhost:44391"
                    },
                    AllowedScopes =
                    {
                        "openid", 
                        "profile", 
                        "email",
                        "ttaapp"
                    },
                    RedirectUris =
                    {
                        "https://localhost:44391/authentication/login-callback"
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://localhost:44391/authentication/logout-callback"
                    },
                    Enabled = true
                },
            };
    }
}