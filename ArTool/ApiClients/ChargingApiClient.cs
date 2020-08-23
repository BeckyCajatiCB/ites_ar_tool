using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArTool.Models;
using NLog;

namespace ArTool.ApiClients
{
    public class ChargingApiClient : ApiClient, IChargingApiClient
    {
        public ChargingApiClient(ApiClientOptions apiOptions, ILogger logger = null) : base(apiOptions, logger)
        {
        }

        public Task<List<CreditCardTransaction>> GetTransactionLogByToken(string token, RequestorInformation requestorInfo)
        {
            var path = $"/v1/transactions/logs?tokenid={token}";
            return GetJsonListAsync<CreditCardTransaction>(path, requestorInfo);
        }
    }
}
