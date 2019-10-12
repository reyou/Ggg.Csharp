using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.IO;
using System.Threading.Tasks;

namespace intro1
{
    /// <summary>
    /// https://www.nuget.org/packages/Microsoft.Azure.DocumentDB/
    /// https://docs.microsoft.com/en-us/azure/cosmos-db/how-to-use-resource-tokens-gremlin#create-a-resource-token
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ConnectionPolicy connectionPolicy = new ConnectionPolicy
            {
                EnableEndpointDiscovery = false,
                ConnectionMode = ConnectionMode.Direct
            };
            // Uri serviceEndpoint = new Uri("https://contoso.documents.azure.com:443/");
            string hostname = File.ReadAllText(@"C:\apikeys\cosmos-db-gremlin\hostname.txt");
            Uri serviceEndpoint = new Uri(hostname);
            string authKey = File.ReadAllText(@"C:\apikeys\cosmos-db-gremlin\authKey.txt");
            DocumentClient client = new DocumentClient(serviceEndpoint, authKey, connectionPolicy);
            // Read specific permission to obtain a token.
            // The token isn't returned during the ReadPermissionReedAsync() call.
            // The call succeeds only if database id, user id, and permission id already exist. 
            // Note that <database id> is not a database name. It is a base64 string that represents the database identifier, for example "KalVAA==".
            // Similar comment applies to <user id> and <permission id>.
            try
            {
                Permission permission = await client.ReadPermissionAsync(UriFactory.CreatePermissionUri("<database id>", "<user id>", "<permission id>"));
                Console.WriteLine("Obtained token {0}", permission.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
    }
}
