using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArTool.Models;
using ArTool.Models.Dtos;
using NLog;

namespace ArTool.ApiClients
{
    public class TellerApiClient : ApiClient, ITellerApiClient
    {
        public TellerApiClient(ApiClientOptions apiOptions, ILogger logger = null) : base(apiOptions, logger)
        {
        }

        public Task<PostPaymentInfoResponse> PostPaymentInfo(ElectronicPaymentInfo paymentDetails, RequestorInformation requestorInfo)
        {
            var path = "/v1/paymentdetails/creditcard";
            return PostJsonAsync<PostPaymentInfoResponse>(path, paymentDetails, requestorInfo);
        }

        public Task<PaymentMethod> PostPaymentMethod(PaymentMethod paymentMethod, RelatesTo relatesTo,RequestorInformation requestorInfo)
        {
            var path = $"/v1/{relatesTo.Type}s/{relatesTo.Did}/paymentmethods";
            return PostJsonAsync<PaymentMethod>(path, paymentMethod, requestorInfo);
        }
    }
}
