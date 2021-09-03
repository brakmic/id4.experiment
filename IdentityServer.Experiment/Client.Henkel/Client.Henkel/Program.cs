using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using IdentityModel.Client;
using static IdentityModel.OidcConstants;

namespace Client.Henkel
{
	class Program
	{
		static async System.Threading.Tasks.Task Main(string[] args)
		{
            // Call PatentSight's IdentityServer to discover endpoints from its metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5010");

            Console.WriteLine("Asking Identity Server for Discovery Document");

            if (disco.IsError)
            {
                Console.WriteLine($"Identity Server Error: {disco.Error}");
                return;
            }

            Console.WriteLine("Asking Identity Server for Credentials Token");

            // Use credentials given by PatentSight to receive a valid access token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "remoteClient",
                ClientSecret = "Pass123??",
                Scope = "api.read",
                GrantType = GrantTypes.ClientCredentials
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine($"Identity Server Error: {tokenResponse.Error}");
                return;
            }

            Console.WriteLine("Identity Server returned a token:\n");

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");
            Console.WriteLine("Inserting token into the Authorization Header");
            // Use the token to query protected APIs
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            Console.WriteLine("Trying to access the API.\n");

            var response = await apiClient.GetAsync("https://localhost:5011/api/customfields");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API Server rejected query:\n\n{response.StatusCode}");
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Server returned the following results:\n\n{JArray.Parse(content)}");
            }

            Console.ReadLine();
        }
	}
}
