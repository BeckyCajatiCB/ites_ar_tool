using ArTool.ApiClients;
using FlurlClientWrapper.Core.AuthenticationStrategies;
using StructureMap;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;

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

                //For<IChargingApiClient>().Use("Create a new charging api client",
                //    e => GetChargingApiClient(
                //        configuration["ChargingApi:ApiBaseUrl"],
                //        configuration["ChargingApi:ClientId"],
                //        configuration["ChargingApi:ClientSecret"],
                //        configuration["ChargingApi:IdentityProvider"]));

                //For<ICompaniesApiClient>().Use("Create a new companies api client",
                //    e => GetCompaniesApiClient(
                //        configuration["CompaniesApi:ApiBaseUrl"],
                //        configuration["CompaniesApi:ClientId"],
                //        configuration["CompaniesApi:ClientSecret"],
                //        configuration["CompaniesApi:IdentityProvider"]));
            }

            //private static IChargingApiClient GetChargingApiClient(string apiBaseUrl, string clientId, string clientSecret, string identityProvider)
            //{
            //    var apiClientOptions = new ApiClientOptions
            //    {
            //        ApiBaseUrl = apiBaseUrl,
            //        TokenHeader = AccessTokenGenerator.GetReferenceTokenHeader(identityProvider, clientId, clientSecret)
            //    };

            //    return new ChargingApiClient(apiClientOptions, ChargingRepository.Logger);
            //}

            //private static ICompaniesApiClient GetCompaniesApiClient(string apiBaseUrl, string clientId, string clientSecret, string identityProvider)
            //{
            //    var apiClientOptions = new ApiClientOptions
            //    {
            //        ApiBaseUrl = apiBaseUrl,
            //        TokenHeader = AccessTokenGenerator.GetReferenceTokenHeader(identityProvider, clientId, clientSecret)
            //    };

            //    return new CompaniesApiClient(apiClientOptions, ChargingRepository.Logger);
            //}
        }

}
