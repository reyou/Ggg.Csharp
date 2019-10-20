using System;
using System.Net.Http;
using System.Threading.Tasks;
using  IdentityModel.Client;
using Newtonsoft.Json;

namespace ClientApplication
{
    /// <summary>
    /// https://identitymodel.readthedocs.io/en/latest/client/overview.html
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            HttpClient client = new HttpClient();
            // Once we have the identityserver object, next step is to get the token.
            DiscoveryDocumentResponse disco = await client.GetDiscoveryDocumentAsync("https://localhost:44376");
            if (disco.IsError)
            {
                Console.Write(disco.Error);
                return;
            }
           
            ClientCredentialsTokenRequest clientCredentialsTokenRequest = new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "Client1",
                ClientSecret = "secret",
                Scope = "api1"
            };
           
            /*So In “tokenResponse” object we have the token, now we are good with calling the api,
             let’s make a call to api by passing this token. We have defined in API , 
             token is of type “Bearer”, so we will add this token to HttpClient method SetBearerToken method*/
            TokenResponse tokenResponse = await client.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);
            Console.WriteLine(JsonConvert.SerializeObject(tokenResponse, Formatting.Indented));
            client.SetBearerToken(tokenResponse.AccessToken);

            HttpResponseMessage response = await client.GetAsync("https://localhost:44357/weatherforecast");
            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            Console.WriteLine("Bye World!");

        }
    }
}
