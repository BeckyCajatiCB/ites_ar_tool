using ArTool.ApiClients;
using FlurlClientWrapper.Core.AuthenticationStrategies;
using StructureMap;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using ArTool.Managers;

namespace ArTool.Configuration
{
        public class BaseRegistry : StructureMap.Registry
    {
            public BaseRegistry(IConfiguration configuration)
            {
                Scan(x =>
                {
                    x.AssemblyContainingType(typeof(BaseRegistry));
                    x.WithDefaultConventions();
                });

            For<IChargingApiClient>().Use("Create a new charging api client",
                e => GetChargingApiClient(
                    configuration["ChargingApi:ApiBaseUrl"],
                    configuration["ChargingApi:ClientId"],
                    configuration["ChargingApi:ClientSecret"],
                    configuration["ChargingApi:IdentityProvider"]));

            For<ITellerApiClient>().Use("Create a new companies api client",
                e => GetTellerApiClient(
                    configuration["TellerApi:ApiBaseUrl"],
                    configuration["TellerApi:ClientId"],
                    configuration["TellerApi:ClientSecret"],
                    configuration["TellerApi:IdentityProvider"]));
        }

        private static IChargingApiClient GetChargingApiClient(string apiBaseUrl, string clientId, string clientSecret, string identityProvider)
        {
            var apiClientOptions = new ApiClientOptions
            {
                ApiBaseUrl = apiBaseUrl,
                TokenHeader = AccessTokenGenerator.GetReferenceTokenHeader(identityProvider, clientId, clientSecret)
            };
            return new ChargingApiClient(apiClientOptions, BackfillManager.Logger);
        }

        private static ITellerApiClient GetTellerApiClient(string apiBaseUrl, string clientId, string clientSecret, string identityProvider)
        {
            var apiClientOptions = new ApiClientOptions
            {
                ApiBaseUrl = apiBaseUrl,
                TokenHeader = AccessTokenGenerator.GetReferenceTokenHeader(identityProvider, clientId, clientSecret)
            };
            return new TellerApiClient(apiClientOptions, BackfillManager.Logger);
        }
    }

}
