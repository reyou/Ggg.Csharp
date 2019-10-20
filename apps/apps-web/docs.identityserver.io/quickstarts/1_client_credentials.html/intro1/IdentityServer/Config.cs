// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids
        {
            get
            {
                return new IdentityResource[]
                {
                    new IdentityResources.OpenId()
                };
            }
        }

        public static IEnumerable<ApiResource> Apis
        {
            get
            {
                return new[]
                {
                    new ApiResource("api1", "My API")
                };
            }
        }

        /*You can think of the ClientId and the ClientSecret as the login and password
         for your application itself. It identifies your application to the 
         identity server so that it knows which application is trying to connect to it.*/
        public static IEnumerable<Client> Clients
        {
            get
            {
                return new[]
                {
                    new Client
                    {
                        ClientId = "client",

                        // no interactive user, use the clientid/secret for authentication
                        AllowedGrantTypes = GrantTypes.ClientCredentials,

                        // secret for authentication
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },

                        // scopes that client has access to
                        AllowedScopes = {"api1"}
                    }
                };
            }
        }
    }
}