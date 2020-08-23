using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArTool.Models;

namespace ArTool.ApiClients
{
    public interface IChargingApiClient
    {
        Task<List<CreditCardTransaction>> GetTransactionLogByToken(string token, RequestorInformation requestorInfo);

    }
}
