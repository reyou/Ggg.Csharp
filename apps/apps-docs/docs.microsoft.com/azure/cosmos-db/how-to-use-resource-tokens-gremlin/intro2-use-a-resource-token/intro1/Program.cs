using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using System;
using System.Threading.Tasks;

namespace intro1
{
    /// <summary>
    /// https://www.nuget.org/packages/Microsoft.Azure.DocumentDB/
    /// https://docs.microsoft.com/en-us/azure/cosmos-db/how-to-use-resource-tokens-gremlin#use-a-resource-token
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // The Gremlin application needs to be given a resource token. It can't discover the token on its own.
            // You can obtain the token for a given permission by using the Azure Cosmos DB SDK, or you can pass it into the application as a command line argument or configuration value.
            string resourceToken = GetResourceToken();

            // Configure the Gremlin server to use a resource token rather than a master key.
            GremlinServer server = new GremlinServer(
                "contoso.gremlin.cosmosdb.azure.com",
                port: 443,
                enableSsl: true,
                username: "/dbs/<database name>/colls/<collection name>",

                // The format of the token is "type=resource&ver=1&sig=<base64 string>;<base64 string>;".
                password: resourceToken);

            using (GremlinClient gremlinClient = new GremlinClient(server, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                await gremlinClient.SubmitAsync("g.V().limit(1)");
            }

            Console.ReadLine();
        }

        private static string GetResourceToken()
        {
            throw new NotImplementedException();
        }
    }
}
