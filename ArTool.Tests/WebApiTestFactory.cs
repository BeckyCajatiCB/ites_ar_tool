using System;
using System.Net.Http;
using IdentityModel.Client;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace ArTool.Tests
{
    public class WebApiTestFactory : WebApplicationFactory<StartupTest>
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<StartupTest>()
                .UseEnvironment("Test");
        }

        protected override void ConfigureClient(HttpClient client)
        {
            base.ConfigureClient(client);

            var idpHandler = Server.CreateHandler();
            Server.Host.Services.GetService<StartupTest>().IdpHandler = idpHandler;

            var tokenResponse = GetTokenResponse(idpHandler);
            client.BaseAddress = new Uri("http://localhost:5000");
            client.SetBearerToken(tokenResponse.AccessToken);
        }

        public TokenResponse GetTokenResponse(HttpMessageHandler idpHandler)
        {
            using (var httpClient = new HttpClient(idpHandler))
            {
                var discoveryResponse = httpClient.GetDiscoveryDocumentAsync("http://localhost:5000").Result;
                var response = httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = discoveryResponse.TokenEndpoint,
                    ClientId = "sample_client",
                    ClientSecret = "client_secret"
                }).Result;

                return response;
            }
        }
    }
}