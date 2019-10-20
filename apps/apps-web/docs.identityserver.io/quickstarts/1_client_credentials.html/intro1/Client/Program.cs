using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
    /// <summary>
    /// https://jwt.io/
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // discover endpoints from metadata
            using (HttpClient client = new HttpClient())
            {
                DiscoveryDocumentResponse disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
                if (disco.IsError)
                {
                    Console.WriteLine(disco.Error);
                    return;
                }
                // request token
                TokenResponse tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "client",
                    ClientSecret = "secret",
                    Scope = "api1"
                });

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    return;
                }

                Console.WriteLine(tokenResponse.Json);
                await CallApi(tokenResponse);
            }
           
            Console.WriteLine("Bye World!");
            Console.ReadLine();
        }

        private static async Task CallApi(TokenResponse tokenResponse)
        {
            // call api
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(tokenResponse.AccessToken);

                HttpResponseMessage response = await client.GetAsync("https://localhost:5001/identity");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(JArray.Parse(content));
                }
            }
        }
    }
}
