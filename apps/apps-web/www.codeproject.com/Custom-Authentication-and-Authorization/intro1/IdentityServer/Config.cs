using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> GetClients()
        {
            // Models an OpenID Connect or OAuth2 client
            // Class:IdentityServer4.Models.Client
            List<Client> clients = new List<Client>();

            //Client1
            clients.Add(new Client
            {
                ClientId = "Client1",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                }
            });
            return clients;
        }

        // Defining the InMemory API's
        public static IEnumerable<ApiResource> GetApiResources()
        {
            List<ApiResource> apiResources = new List<ApiResource>();
            apiResources.Add(new ApiResource("api1", "First API"));
            return apiResources;
        }
    }
}
